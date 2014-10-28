using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediCare.NetworkLibrary
{
    /// <summary>
    /// ClientTcpConnector.
    /// 
    /// This class is used by the Client, DoctorsClient and SignupTool to connect to the server
    /// </summary>
    public class ClientTcpConnector
    {
        private TcpClient _client;
        private String _server;

        private SslStream stream;
        private BlockingCollection<Packet> sendQueue = new BlockingCollection<Packet>(new ConcurrentQueue<Packet>());
      
        public ClientTcpConnector(TcpClient client, String server)
        {
            this._client = client;
            this._server = server;

            //SSL Stream
            stream = new SslStream(client.GetStream(), false,
            new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            stream.AuthenticateAsClient(server);

            StartClientHelper();
        }

        //Method for clients to use to send messages to the server
        //First part of the method is old code. second part is new to be used when SSL is working
        public void sendMessage(Packet packet)
        {
            sendQueue.Add(packet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Packet ReadMessage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String dataString = (String)formatter.Deserialize(stream);
            return Utils.GetPacket(dataString);
        }

        private void StartClientHelper()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var p = sendQueue.Take();

                    if (stream != null && stream.CanWrite && p != null)
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, Utils.GetPacketString(p));
                    }
                }
            }).Start();
        }
        public void Close()
        {
            _client.Close();
        }

        public void sendFirstConnectPacket(string id, string password)
        {
            sendMessage(new Packet(id, "FirstConnect", "server", id + ":" + password));
        }

        public Boolean isConnected()
        {
            return _client.Connected;
        }

        #region ssl validator
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        // This allows you to check the certificate and accept or reject it
        // return true will accept the certificate
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept all certificates
            return true;
        }
        # endregion
    }
}
