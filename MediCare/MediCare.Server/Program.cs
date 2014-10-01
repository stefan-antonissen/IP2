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
            try
            {
                logins.LoadLogins();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
                    while (true)
                    {

                        String dataString = "";
                        Packet packet = null;
                        ;
                        if (sender.Connected)
                        {
                            dataString = (String)formatter.Deserialize(sender.GetStream());
                            packet = Utils.GetPacket(dataString);

                            //Console.WriteLine(dataString);

                            // if (!clients.ContainsKey(packet.GetID()))
                            //{
                            //    clients.Add(packet.GetID(), incomingClient);
                            // }

                            Console.WriteLine("Client connected");
                            switch (packet._type)
                            {
                                //sender = incoming client
                                //packet = data van de client
                                case "Chat":
                                HandleChatPacket(packet);
                                break;
                                case "FirstConnect":
                                HandleFirstConnectPacket(packet, sender);
                                break;
                                case "Disconnect":
                                HandleDisconnectPacket(packet, sender);
                                break;
                                case "Data":
                                HandleDataPacket(packet, sender);
                                break;
                                case "Registration":
                                HandleRegistrationPacket(packet, sender);
                                break;
                                case "Broadcast":
                                HandleBroadcastMessagePacket(packet);
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
            try
            {
                TcpClient destination = clients[packet.GetDestination()];
                SendPacket(destination, packet);
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
        private void HandleFirstConnectPacket(Packet p, TcpClient sender)
        {
            Packet response = new Packet("Server", "FirstConnect", p._id, "VERIFIED");
            SendPacket(sender, response);
        }

        /**
         * Stuur het sluit bericht terug naar de client en sluit de connectie. 
         * Client doet hetzelfde na het ontvangen van het sluit bericht.
         */
        private void HandleDisconnectPacket(Packet p, TcpClient sender)
        {
            Packet response = new Packet("server", "Disconnect", p.GetID(), "LOGGED OFF");
            SendPacket(sender, response);
            Console.WriteLine(p.GetID() + " has disconnected");
            sender.Close();
        }

        /**
         * Save de data die je binnen krijgt.
         * Stuur de data door naar de DokorClient.
         */
        private void HandleDataPacket(Packet packet, TcpClient sender)
        {
            Packet response_Sender = new Packet("Server", "Data", packet._id, "Data Saved");
            SendPacket(sender, response_Sender);

            Packet response_receiver = new Packet(packet.GetDestination(), "data", packet.GetID(), packet.GetMessage());
            try
            {
                TcpClient destination = clients[packet.GetDestination()];
                SendPacket(destination, packet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //TODO: save some data here locally on the server
        }

        /**
         * Handel het registratie process af. genereer een uniek ID voeg toe aan bestand (zie LoginIO)
         * Stuur een bericht terug dat de data is aangekomen.
         */
        private void HandleRegistrationPacket(Packet p, TcpClient sender)
        {
            logins.add(p.GetMessage());
            Packet response = new Packet("server", "Registration", p.GetID(), "Registration attempt succeeded");
            SendPacket(sender, response);
        }

        /**
         * Handel een broadcast message af van de Doktor.
         * 
         */
        private void HandleBroadcastMessagePacket(Packet p)
        {
            Packet response = new Packet();
        }

        private void SendPacket(TcpClient client, Packet p)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(client.GetStream(), Utils.GetPacketString(p));
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
