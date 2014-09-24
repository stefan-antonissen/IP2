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

namespace MediCare.Server
{
    class Program : ServerInterface
    {
        private IPAddress _localIP = IPAddress.Parse("127.0.0.1");
        private Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            TcpListener server = new TcpListener(_localIP, 11000);
			server.Start();
            TcpClient incomingClient;
			while (true)
			{
				incomingClient = server.AcceptTcpClient();

				new Thread(() =>
				{
					BinaryFormatter formatter = new BinaryFormatter();
                    TcpClient temp = incomingClient;
					while (true)
					{
                        Packet packet = (Packet)formatter.Deserialize(incomingClient.GetStream());
                        if (!clients.ContainsKey(packet.GetID()))
                        {
                            clients.Add(packet.GetID(), incomingClient);
                        }
                        if (packet.GetType().Equals("Client"))
                        {
                            if (packet.GetType().Equals("chat"))
                            {
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
            string temp = id.Substring(0,1);
            switch(temp)
            {
                case "0": return "Doctor";
                case "1": return "Client";
                default: Console.WriteLine("ERROR, UNKNOWN ID"); 
                    return "";
            }
        }
       }
    
}
