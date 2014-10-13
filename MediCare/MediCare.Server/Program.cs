using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using MediCare.NetworkLibrary;
using MediCare.DataHandling;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MediCare.Server
{
    class Server
    {
        private IPAddress _localIP = IPAddress.Parse("127.0.0.1");
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private Dictionary<string, SslStream> clientsStreams = new Dictionary<string, SslStream>();
        private ObjectIOv2 mIOv2; // do not remove, do not move and do not edit!
        
        private LoginIO logins = new LoginIO();

        private string _toAllDoctors = "Dokter";
        //README!!! - SSL certificate needs to be coppied from MediCare.Server\ssl_cert.pfx to C:\Windows\Temp\
        private X509Certificate certificate = new X509Certificate(@"C:\Windows\Temp\ssl_cert.pfx", "medicare");


        static void Main(string[] args)
        {
            new Server();
        }

        public Server()
        {
            mIOv2 = new ObjectIOv2(); // do not remove, do not move and do not edit!

            TcpListener server = new TcpListener(_localIP, 11000);
            server.Start();

            TcpClient incomingClient;
            Console.WriteLine("Waiting for connection...");
            while (true)
            {
                incomingClient = server.AcceptTcpClient();
                
                new Thread(() =>
                {
                    Console.WriteLine("Connection found!");
                    BinaryFormatter formatter = new BinaryFormatter();
                    TcpClient sender = incomingClient;
                    SslStream sslStream = new SslStream(incomingClient.GetStream());
                    sslStream.AuthenticateAsServer(certificate);

                    while (true)
                    {

                        String dataString = "";
                        Packet packet = null;
                        if (sender.Connected)
                        {
                            dataString = (String)formatter.Deserialize(sslStream);
                            //dataString = (String)formatter.Deserialize(sender.GetStream());
                            packet = Utils.GetPacket(dataString);

                            //Console.WriteLine(dataString);

                            if (!clients.ContainsKey(packet._id))
                            {
                                clients.Add(packet.GetID(), incomingClient);
                                clientsStreams.Add(packet.GetID(), sslStream);
                                
                                #region DEBUG
#if DEBUG
                                Console.WriteLine("ID: " + packet.GetID() + "incomingClient: " + incomingClient.ToString());
                                printClientList();
#endif
                                #endregion
                            }

                            Console.WriteLine("Client connected");
                            switch (packet._type)
                            {
                                //sender = incoming client
                                //packet = data van de client
                                case "Chat":
                                HandleChatPacket(packet);
                                break;
                                case "FirstConnect":
                                HandleFirstConnectPacket(packet, sslStream);
                                break;
                                case "Disconnect":
                                HandleDisconnectPacket(packet, sslStream);
                                break;
                                case "Data":
                                HandleDataPacket(packet, sslStream);
                                break;
                                case "Registration":
                                HandleRegistrationPacket(packet, sslStream);
                                break;
                                case "Broadcast":
                                HandleBroadcastMessagePacket(packet);
                                break;
                                case "Timestamp":
                                HandleTimestampPacket(packet);
                                break;
                                case "ActiveClients":
                                HandleActiveClients(packet, sslStream);
                                break;
                                case "Filelist":
                                HandleFileList(packet, sslStream);
                                break;
                                case "FileRequest":
                                HandleFileRequest(incomingClient, packet);
                                break;
                                default: //nothing
                                break;
                            }
                        }
                    } // end While
                }).Start();
            }

        }

        /**
         * Versturen van een chat, package blijft hetzelfde.
         * Methode is er omdat het anders uit de toon valt met andere methodes.
         */
        private void HandleChatPacket(Packet packet)
        {
            if (IsDoctor(packet._id)) //if source is doctor send the message to the destination.
            {
                sendToDestination(packet);
            }
            else //else: source is client. send to all the connected doctors. (connections with an id who start with 9)
            {
                sendToDoctors(packet);
            }
        }

        /// <summary>
        /// chathelpers are used by the handleChatPacket method.
        /// contents: sendToDoctors(Packet packet):void; sendToDestination(packet):void and isDoctor(String id):bool
        /// </summary>
        #region chathelpers
        private void sendToDoctors(Packet packet)
        {
            foreach (KeyValuePair<string, SslStream> entry in clientsStreams)
            {
                if (IsDoctor(entry.Key))
                {
                    SslStream sslStream = entry.Value;
                    SendPacket(sslStream, packet);
                    //Console.WriteLine("packetMessage: " + packet._message);
                }
            }
        }

        private void sendToDestination(Packet packet)
        {
            SslStream sslStream;
            clientsStreams.TryGetValue(packet._destination, out sslStream);
            SendPacket(sslStream, packet);
        }

        private bool IsDoctor(String id)
        {
            return id.Substring(0, 1).Contains("9");
        }

        #endregion

        /**
         * Zoeken in de *database* naar de juiste persoons gegevens en haal daar het correcte ID op
         * 
         */
        private void HandleFirstConnectPacket(Packet p, SslStream stream)
        {
            Packet response = new Packet("Server", "FirstConnect", p._id, "VERIFIED");
            SendPacket(stream, response);
        }

        /**
         * Stuur het sluit bericht terug naar de client en sluit de connectie. 
         * Client doet hetzelfde na het ontvangen van het sluit bericht.
         */
        private void HandleDisconnectPacket(Packet p, SslStream stream)
        {
            mIOv2.Remove_client(p); // do not remove, do not move and do not edit!
            Packet response = new Packet("server", "Disconnect", p.GetID(), "LOGGED OFF");
            SendPacket(stream, response);
            Console.WriteLine(p.GetID() + " has disconnected");
            TcpClient sender;
            clients.TryGetValue(p._id, out sender);
            sender.Close();
        }

        /**
         * Save de data die je binnen krijgt.
         * Stuur de data door naar de DoktorClient.
         */
        private void HandleDataPacket(Packet packet, SslStream stream)
        {
            SaveMeasurement(packet);
            Packet response_Sender = new Packet("Server", "Data", packet._id, "Data Saved");
            SendPacket(stream, response_Sender);
            SslStream sslStream;
            if (packet._destination == _toAllDoctors)
            {
                foreach (var s in clientsStreams)
                {
                    if (s.Key.StartsWith("9"))
                    {
                        Console.WriteLine("Destination: " + s.Key);// + sslStream.ToString());
                        try
                        {
                            SendPacket(s.Value, packet);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            else
            {
                clientsStreams.TryGetValue(packet._destination, out sslStream); 
                Console.WriteLine("Destination: " + packet._destination);// + sslStream.ToString());
                try
                {
                    SendPacket(sslStream, packet);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /**
        * Handel het registratie process af. genereer een uniek ID voeg toe aan bestand (zie LoginIO)
        * Stuur een bericht terug dat de data is aangekomen.
        */
        private void HandleTimestampPacket(Packet p)
        {
            SaveMeasurement(p);
        }
        /**
         * Handel het registratie process af. genereer een uniek ID voeg toe aan bestand (zie LoginIO)
         * Stuur een bericht terug dat de data is aangekomen.
         */
        private void HandleRegistrationPacket(Packet p, SslStream stream)
        {
            logins.add(p.GetMessage());
            Packet response = new Packet("server", "Registration", p.GetID(), "Registration attempt succeeded");
            SendPacket(stream, response);
        }

        /**
         * Handel een broadcast message af van de Doktor.
         * 
         */
        private void HandleBroadcastMessagePacket(Packet p)
        {
            Packet response = new Packet();
        }
        /*
         * Geeft het aantal actieve clients
         * 
         */
        private void HandleActiveClients(Packet p, SslStream stream)
        {
            string ids = "";
            foreach (string key in clients.Keys)
            {
                if (!key.StartsWith("9"))
                {
                    ids += key + " ";
                }
            }
            Console.WriteLine("Active clients: " + clients.Count.ToString());
            Packet response = new Packet("Server", "ActiveClients", p._id, ids);
            SendPacket(stream, response);
        }
        /// <summary>
        /// Methode die aangeroepen wordt als de server een request voor de files binnenkrijgt
        /// </summary>
        /// <param name="packet">Packet waarin de message het id van de opgevraagde patient moet zijn</param>
        /// <param name="stream"></param>
        private void HandleFileList(Packet packet, SslStream stream)
        {
            Packet response = mIOv2.Get_Files(packet); 
            SslStream sslStream;
            clientsStreams.TryGetValue(packet._destination, out sslStream);
            Console.WriteLine("THIS IS THE RESPONSE PACKET " + response.toString());
            try
            {
                SendPacket(sslStream, response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HandleFileRequest(TcpClient client, Packet packet)
        {
            string FileRequested = packet._message;
            //client.Client.SendFile(mIOv2.Get_File(FileRequested));
            Console.WriteLine(mIOv2.Get_File(FileRequested));
        }

        private void SendPacket(SslStream stream, Packet p)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Utils.GetPacketString(p));
        }

        private void SaveMeasurement(Packet p)
        {
            /*
             * eerste packet is altijd een timestamp
             * daarna:
             * 7 strings in de array:
                Heartbeat = data[0];
                RPM = data[1];
                Speed = data[2];
                Distance = data[3];
                Power = data[4];
                Energy = data[5];
                TimeRunning = data[6];
                Brake = data[7];
            */
            string[] data = (p.GetMessage().Split(' '));
            if (data.Length < 8)
            {
                mIOv2.Create_file(p);
            }
            else
            {
                //Console.WriteLine("\nHeartbeat: " + data[0] + "\nRPM 1: " + data[1] + "\nSpeed 2: " + data[2] + "\nDistance 3: " + data[3] +
                //    "\nPower 4: " + data[4] + "\nEnergy 5: " + data[5] + "\nTimeRunning 6: " + data[6] + "\nBrake 7: " + data[7]);
                mIOv2.Add_Measurement(p);
            }
        }

        private void printClientList()
        {
            Console.WriteLine("\n############################################# ");
            Console.WriteLine("content for clients dictionary: ");
            foreach (KeyValuePair<string, TcpClient> entry in clients)
            {
                Console.WriteLine("ID: " + ((string)entry.Key) + " TcpClient: " + entry.Value.GetHashCode());
            }
            Console.WriteLine("#############################################\n");
        }

        private string ResolveID(string id)
        {
            string temp = id.Substring(0, 1);
            switch (temp)
            {
                case "9":
                return "Doctor";
                default:
                return "Client";
            }
        }
    }
}
