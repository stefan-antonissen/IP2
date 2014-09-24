using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.NetworkLibrary
{
    [Serializable()]
    public class Packet
    {
        private string data;

        public Packet(string data)
        {
            this.data = data;
        }

        public void handleServer(TcpClient tcpClient, ServerInterface server)
        {
            server.send(tcpClient);
        }

        public void handleClient(ClientInterface client)
        {
            client.send(data);
        }
    }
}
