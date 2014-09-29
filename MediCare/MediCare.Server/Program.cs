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

namespace MediCare.Server
{
    class Program //: ServerInterface
    {
        private IPAddress _localIP = IPAddress.Parse("127.0.0.1");
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        private LoginIO logins = new LoginIO();

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            try
            {
                logins.LoadLogins();
            } catch(System.IO.FileNotFoundException e) {
                logins.SaveLogins();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
                    TcpClient temp = incomingClient;
                    while (true)
                    {
                        //temp.GetStream().Position = 0;
                        Packet packet = (Packet)formatter.Deserialize(temp.GetStream());
                        if (!clients.ContainsKey(packet.GetID()))
                        {
                            clients.Add(packet.GetID(), incomingClient);
                        }
                        if (ResolveID(packet.GetID()).Equals("Client"))
                        {
                            Console.WriteLine("Client connected");
                            if (packet.GetType().Equals("chat"))
                            {
                                Console.WriteLine(packet.toString());
                                TcpClient dest = clients[packet.GetDestination()];
                                SendPacket(dest, packet);
                            }
                            else
                            {
                                // save data to database
                                TcpClient dest = clients[packet.GetDestination()];
                                SendPacket(dest, packet);
                            }
                        }
                        else
                        {
                            //string packetString;   // create string out of packet with JSON
                            TcpClient dest = clients[packet.GetDestination()];
                            SendPacket(dest, packet);
                        }
                    }
                }).Start();
            }
        }


        private void SendPacket(TcpClient client, Packet p)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(client.GetStream(), p);
        }

        private string ResolveID(string id)
        {
            string temp = id.Substring(0, 1);
            switch (temp)
            {
                case "9": return "Doctor";
                default: return "Client";
            }
        }
    }
}
