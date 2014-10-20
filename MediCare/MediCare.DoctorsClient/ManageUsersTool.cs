using MediCare.NetworkLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare
{
    public partial class ManageUsersTool : Form
    {
        private readonly Timer _labelRemoveTimer;
        private static string _server = "127.0.0.1";
        private static int _port = 11000;
        private ClientTcpConnector _client;
        private string _id;

        public ManageUsersTool(string id)
        {
            //TODO HANDLE DISCONNECT
            InitializeComponent();

            //verbinden met de server om registratie af te handelen
            TcpClient TcpClient = new TcpClient(_server, _port);
            _client = new ClientTcpConnector(TcpClient, _server);

            this._id = id;

            _labelRemoveTimer = new Timer();
            _labelRemoveTimer.Interval = 3000;
            _labelRemoveTimer.Tick += UpdateLabel;

            this.FormClosing += ManageUsersTool_FormClosing;

            _client.sendFirstConnectPacket(id + "r", "nopassword");
            Console.WriteLine(_client.ReadMessage());
        }

       private void ManageUsersTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void UpdateLabel(object sender, EventArgs e)
        {
            Error_Label.Text = "";
            _labelRemoveTimer.Stop();
        }
    }
}
