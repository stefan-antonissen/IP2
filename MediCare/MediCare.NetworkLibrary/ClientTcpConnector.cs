using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.NetworkLibrary
{
    public class ClientTcpConnector
    {
        TcpClient _client;
        String _server;

        SslStream stream;

        public ClientTcpConnector(TcpClient client, String server)
        {
            this._client = client;
            this._server = server;

            //SSL Stream
            stream = new SslStream(client.GetStream() , false ,
            new RemoteCertificateValidationCallback(ValidateServerCertificate), null );
            stream.AuthenticateAsClient(server);
        }

        //Method for clients to use to send messages to the server
        //First part of the method is old code. second part is new to be used when SSL is working
        public void sendMessage(Packet packet)
        {
            //SSL Stream
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Utils.GetPacketString(packet)); // the serialization process
        }

        public Packet ReadMessage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String dataString = (String)formatter.Deserialize(stream);
            return Utils.GetPacket(dataString);
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
