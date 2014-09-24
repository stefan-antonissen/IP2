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

namespace MediCare.Server
{
    class Program
    {
        private IPAddress _localIP;
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            _localIP = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(_localIP, 11000);

			server.Start();
			while (true)
			{
				TcpClient tcpClient = server.AcceptTcpClient();

				new Thread(() =>
				{
					BinaryFormatter formatter = new BinaryFormatter();
					while (true)
					{
						//Packet packet = (Packet)formatter.Deserialize(tcpClient.GetStream());
						//packet.handleServer(tcpClient, this);
					}
				}).Start();
			}
		}


		void sendPacket(TcpClient client, String p)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(client.GetStream(), p);
		}
       }
    
}
