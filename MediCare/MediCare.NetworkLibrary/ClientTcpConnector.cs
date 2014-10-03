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

        NetworkStream strm;
        private TcpClient client;
        private string server;

        public ClientTcpConnector(TcpClient client, String server)
        {
            this._client = client;
            this._server = server;

            //Non ssl stream
            strm = client.GetStream(); // the stream
        }

        //Method for clients to use to send messages to the server
        //First part of the method is old code. second part is new to be used when SSL is working
        public void sendMessage(Packet packet)
        {
            //legacy mode
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(strm, Utils.GetPacketString(packet)); // the serialization process 


            //ssl stream
    //        using (SslStream sslStream = new SslStream(_client.GetStream(), false,
    //new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
    //        {
    //            sslStream.AuthenticateAsClient(_server);
    //            byte[] byeArray = Encoding.UTF8.GetBytes(Utils.GetPacketString(packet));
    //            sslStream.Write(byeArray);
    //        }
            //endof sslstream
        }

        public Packet ReadMessage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String dataString = (String)formatter.Deserialize(client.GetStream());
            return Utils.GetPacket(dataString);


            //ssl stream
    //        using (SslStream sslStream = new SslStream(_client.GetStream(), false,
    //new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
    //        {
    //            sslStream.AuthenticateAsClient(_server);
    //            //TODO read bytes put to string and get packet from json string
    //        }
            //endof sslstream
        }

        public void Close()
        {
            client.Close();
        }

        public Boolean isConnected()
        {
            return client.Connected;
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
