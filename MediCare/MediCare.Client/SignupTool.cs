using MediCare.NetworkLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare
{
    public partial class SignupTool : Form
    {
        public SignupTool()
        {
            InitializeComponent();
        }

        /**
         * use Secure Socket to send user registration data to server.
         */
        private void on_Submit_Event(Object sender, EventArgs e)
        {
            if(Username_TextBox.Text != "" && Password_TextBox.Text != "" && Password_TextBox.Text.Equals(Password_Verify_TextBox.Text)) {
                string name = Username_TextBox.Text;
                string pass = Password_TextBox.Text;
                Username_TextBox.Text = "";
                Password_TextBox.Text = "";
                Password_Verify_TextBox.Text = "";
            }
            
            // Compile a packet (not defined what to send yet)
            // Do you want to encrypt the password here or on the server? My guess is server.
            // Does the password need to meet certain requirements?
            // Send to server
        }

        private void SendMessageToServer(TcpClient client, Packet message)
        {
            BinaryFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = client.GetStream(); // the stream
            MessageBox.Show(message.toString());
            MessageBox.Show(Utils.GetPacketString(message));
            formatter.Serialize(strm, Utils.GetPacketString(message)); // the serialization process 
            //client.GetStream().Write(bytes, 0, bytes.Length);
        }

        private Packet ReadMessage(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String dataString = (String)formatter.Deserialize(client.GetStream());
            return Utils.GetPacket(dataString);
        }
    }
}
