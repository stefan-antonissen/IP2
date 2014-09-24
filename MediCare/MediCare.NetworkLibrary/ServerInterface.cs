using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.NetworkLibrary
{
    public interface ServerInterface
    {
        void send(TcpClient client);
    }
}
