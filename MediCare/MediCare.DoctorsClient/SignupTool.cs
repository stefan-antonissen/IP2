using MediCare.DataHandling;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare
{
    public partial class SignupTool : Form
    {
        private readonly Timer labelRemoveTimer;
        private static string server = "127.0.0.1";
        private static int port = 11000;
        private ClientTcpConnector client;
        private string id;
        public SignupTool(string id)
        {
            InitializeComponent();

            //verbinden met de server om registratie af te handelen
            TcpClient TcpClient = new TcpClient(server, port);
            client = new ClientTcpConnector(TcpClient, server);
            this.id = id;
            labelRemoveTimer = new Timer();
            labelRemoveTimer.Interval = 3000;
            labelRemoveTimer.Tick += UpdateLabel;

            client.sendFirstConnectPacket(id + "r", "nopassword");
        }

        #region Key events

        private void on_username_box_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Username_TextBox.Focused)
            {
                this.ActiveControl = Password_TextBox;
            }
        }
        private void on_password_box_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Password_TextBox.Focused)
            {
                this.ActiveControl = Password_Verify_TextBox;
            }
        }
        private void on_verify_password_box_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Password_Verify_TextBox.Focused)
            {
                login(sender, e); // pass it on to the follow up event
            }
        }
        #endregion

        #region Register stuff
        // use Secure Socket to send user registration data to server
        /**
         * TODO: Logging in for real on the server
         */
        private void login(object sender, EventArgs e)
        {
            Regex r = new Regex("^[0-9]{8}$");
            if (Username_TextBox.Text.Equals("") || Password_TextBox.Text.Equals("") || Password_Verify_TextBox.Equals(""))
            {
                Error_Label.Text = "One or more fields are blank!";
                this.ActiveControl = Username_TextBox;
                labelRemoveTimer.Start();
            }
            else if (!Password_TextBox.Text.Equals(Password_Verify_TextBox.Text))
            {
                Error_Label.Text = "Passwords do not match!";
                this.ActiveControl = Password_TextBox;
                labelRemoveTimer.Start();
            }
            //  else if (loginIO.UserExist(Username_TextBox.Text))
            // {
            //      Error_Label.Text = "User aleady exists!";
            //     this.ActiveControl = Username_TextBox;
            //    labelRemoveTimer.Start();
            // }
            else if ((!r.IsMatch(Username_TextBox.Text) || Int32.Parse(Username_TextBox.Text.Substring(0, 1)) == 9))
            {
                Error_Label.Text = "Username must start with 1-8 \n and are 8 characters long";
                this.ActiveControl = Username_TextBox;
                labelRemoveTimer.Start();
            }
            else if (Username_TextBox.Text != "" && Password_TextBox.Text != "" && Password_TextBox.Text.Equals(Password_Verify_TextBox.Text))
            {
                string name = Username_TextBox.Text;
                string pass = Password_TextBox.Text;

                client.sendMessage(new Packet(id + "r", "Registration", "Server", name + ":" + pass));

                MessageBox.Show("Registered user " + name + " and saved! " + client.ReadMessage()._message);

                client.sendMessage(new Packet(id + "r", "Disconnect", "Server", "Disconnecting"));
                if (client.ReadMessage().Equals("LOGGED OFF"))
                {
                    client.Close();
                }
                this.Close();
            }
        }


        #endregion

        #region TCP tools
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
        #endregion

        private void UpdateLabel(object sender, EventArgs e)
        {
            Error_Label.Text = "";
            labelRemoveTimer.Stop();
        }
    }

}
