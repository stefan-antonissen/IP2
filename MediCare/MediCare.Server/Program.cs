using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCare.Controller;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using MediCare.NetworkLibrary;
using System.Collections;
using MediCare.DataHandling;
using System.Web.Script.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace MediCare.Server
{
    class Server
    {
        private IPAddress _localIP = IPAddress.Parse("127.0.0.1");
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private LoginIO logins = new LoginIO();

        static void Main(string[] args)
        {
            new Server();
        }

        public Server()
        {
            TcpListener server = new TcpListener(_localIP, 11000);
            server.Start();

            TcpClient incomingClient;
            Console.WriteLine("Waiting for connection...");
            while (true)
            {
                incomingClient = server.AcceptTcpClient();
                //README!!! - SSL certificate needs to be coppied from MediCare.Server\ssl_cert.pfx to C:\Windows\Temp\
                X509Certificate certificate = new X509Certificate(@"C:\Windows\Temp\ssl_cert.pfx", "medicare");

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

                            if (!clients.ContainsKey(packet.GetID()))
                            {
                                clients.Add(packet.GetID(), incomingClient);
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
                                HandleChatPacket(packet, sslStream);
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
                                default: //nothing
                                break;
                            }
                        }
                    } // end While
                }).Start();
            }

        }

        //get dataString from the ssl socket
        private string ReadStream(SslStream stream)
        {
            String jsonString = "";

            return jsonString;
        }

        /**
         * Versturen van een chat, package blijft hetzelfde.
         * Methode is er omdat het anders uit de toon valt met andere methodes.
         */
        private void HandleChatPacket(Packet packet, SslStream stream)
        {
            try
            {
                SendPacket(stream, packet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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
            Packet response = new Packet("server", "Disconnect", p.GetID(), "LOGGED OFF");
            SendPacket(stream, response);
            Console.WriteLine(p.GetID() + " has disconnected");
            TcpClient sender = clients[p.GetDestination()];
            sender.Close();
        }

        /**
         * Save de data die je binnen krijgt.
         * Stuur de data door naar de DoktorClient.
         */
        private void HandleDataPacket(Packet packet, SslStream stream)
        {
            Packet response_Sender = new Packet("Server", "Data", packet._id, "Data Saved");
            SendPacket(stream, response_Sender);

            Packet response_receiver = new Packet(packet.GetDestination(), "Data", packet.GetID(), packet.GetMessage());

            try
            {
                SendPacket(stream, response_receiver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            SaveMeasurement(packet);
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
                Console.WriteLine("\nTimestamp: " + data[0] + " " + data[1]);
            }
            else
            {
                Console.WriteLine("\nHeartbeat: " + data[0] + "\nRPM 1: " + data[1] + "\nSpeed 2: " + data[2] + "\nDistance 3: " + data[3] +
                    "\nPower 4: " + data[4] + "\nEnergy 5: " + data[5] + "\nTimeRunning 6: " + data[6] + "\nBrake 7: " + data[7]);
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
