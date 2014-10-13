using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediCare.Controller;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using MediCare.NetworkLibrary;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MediCare.DataHandling;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace MediCare.Client
{
    public partial class ClientGui : Form
    {
        private Controller.BikeController bikeController;
        Graph graph;
        private static string server = "127.0.0.1";
        private static int port = 11000;
        private ClientTcpConnector client;
        string ID;

        private readonly Timer updateDataTimer;
        private readonly Timer labelRemoveTimer;
        private Series[] ChartData = new Series[8];

        private string _defaultDestination = "Dokter";

        private bool first = true;
        public ClientGui()
        {
            InitializeComponent();
            graph = new Graph();
            graph.Initialize_Checkboxes_Client();
            graph.InitializeChart_Client();
            graph.InitializeGraph();
            AddGraphToForm();

            this.FormClosing += on_Window_Closed_Event;
            setVisibility(false);

            // update waarden met de data van de fiets
            updateDataTimer = new Timer();
            updateDataTimer.Interval = 500;
            updateDataTimer.Tick += UpdateGUI;

            // timer voor het verwijderen van de errortekst, 'cosmetisch'
            labelRemoveTimer = new Timer();
            labelRemoveTimer.Interval = 3000;
            labelRemoveTimer.Tick += UpdateLabel;

            Connect("");

            //opzetten tcp connectie
            TcpClient TcpClient = new TcpClient(server, port);
            client = new ClientTcpConnector(TcpClient, server);

            new System.Threading.Thread(() =>
            {
                while (true)
                {
                    Packet packet = null;
                    if (client.isConnected())
                    {
                        Console.WriteLine("Reading message\n");
                        packet = client.ReadMessage();

                        if (packet != null)
                        {
                            processPacket(packet);
                        }
                    }
                }
            }).Start();
        }

        private void processPacket(Packet p)
        {
            Console.WriteLine("Type: " + p._type);
            Console.WriteLine("Received packet with message: " + p._message);
            switch (p._type)
            {
                //sender = incoming client
                //packet = data van de client
                case "Chat":
                    HandleChatPacket(p);
                    break;
                case "Command":
                    HandleCommandPacket(p);
                    break;
                default: //nothing
                    break;
            }
        }

        private void HandleCommandPacket(Packet p)
        {
            if (p._message == "reset")
            {
                bikeController.ResetBike();
            }
            else
            {
                bikeController.SetPower(int.Parse(p._message));
            }
        }

        private void HandleChatPacket(Packet p)
        {
            on_message_receive_event(p._message);
        }

        private void Connect(String SelectedPort)
        {
            if (SelectedPort.Equals(""))
            {
                bikeController = new BikeController("");
            }
            else if (SelectedPort.Equals("SIM"))
            {
                bikeController = new BikeController("SIM"); // sim is for testing methods
            }
            else
            {
                bikeController = new BikeController(SelectedPort);
            }
        }

        // onderstaande drie methodes zijn om de waarden in de GUI aan te passen
        private void updateValues(String[] data)
        {
            if (first)
            {
                string[] timestamp = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss").Split();
                SendMeasurementData(timestamp, "Timestamp");
                client.sendMessage(new Packet(ID, "Filelist", "98765432", "12345678"));
                first = false;
            }

            // als de lengte van de data array één is (error), dan zet je alles op 0
            if (data.Length < 8)
            {
                Heartbeats_Box.Text = "0";
                RPM_Box.Text = "0";
                Speed_Box.Text = "0";
                Distance_Box.Text = "0";
                Power_Box.Text = "0";
                Energy_Box.Text = "0";
                TimeRunning_Box.Text = "0";
                Brake_Box.Text = "0";
            }
            // anders pak je de waarden van de bike
            else
            {
                Heartbeats_Box.Text = data[0];
                RPM_Box.Text = data[1];
                Speed_Box.Text = data[2];
                Distance_Box.Text = data[3];
                Power_Box.Text = data[4];
                Energy_Box.Text = data[5];
                TimeRunning_Box.Text = data[6];
                Brake_Box.Text = data[7];
                SendMeasurementData(data, "Data");
                graph.process_Graph_Data(data);
            }
        }

        //TODO Destination is hard-coded
        private void SendMeasurementData(string[] data, string type)
        {
            string s;
            if (data.Length < 8)
            {
                s = data[0] + " " + data[1];
            }
            else
            {
                s = data[0] + " " + data[1] + " " + data[2] + " " + data[3] + " " + data[4] + " " + data[5] + " " + data[6] + " " + data[7];
            }
            Packet p = new Packet(ID, type, _defaultDestination, s);
            if (client.isConnected())
            {
                client.sendMessage(p);
            }
        }

        private async void UpdateGUI(object sender, EventArgs e)
        {
            var result = await DoWorkAsync();
            updateValues(result);
        }

        private async Task<string[]> DoWorkAsync()
        {
            await Task.Delay(100); // 0.1s delay voor het starten van de update, ook eventueel nog aanpassen
            // hieronder test code om de update te testen, deze methode moet de string array returnen van c.getStatus()
            //Random r = new Random();
            //string num = r.Next(1, 100).ToString();
            //string[] str = new string[] { num, num, num, num, num, num, num, num };
            // return str;
            return bikeController.GetStatus();
        }

        # region Chat Box
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            //kan weg?
        }

        // ID = id van sender; type = type bericht; destination = ID van ontvanger; message = bericht
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (typeBox.Text != "")
            {
                Packet p = new Packet(ID, "Chat", _defaultDestination, typeBox.Text);
                client.sendMessage(p);
                txtLog.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
                txtLog_AlignTextToBottom();
                txtLog_ScrollToBottom();
                typeBox.Text = "";
            }
        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (typeBox.Text != "")
                {
                    Packet p = new Packet(ID, "Chat", _defaultDestination, typeBox.Text);
                    client.sendMessage(p);
                    txtLog.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
                    txtLog_AlignTextToBottom();
                    txtLog_ScrollToBottom();
                    typeBox.Text = "";
                }
            }
        }

        private void txtLog_AlignTextToBottom()
        {
            int visibleLines = (int)(txtLog.Height / txtLog.Font.GetHeight()) - 50;
            if (visibleLines > txtLog.Lines.Length)
            {
                int emptyLines = (visibleLines - txtLog.Lines.Length);
                for (int i = 0; i < emptyLines; i++)
                {
                    txtLog.Text = (Environment.NewLine + txtLog.Text);
                }
            }
        }

        private void txtLog_ScrollToBottom()
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        public void on_message_receive_event(string _message)
        {
            txtLog.AppendText(Environment.NewLine + "Other: " + typeBox.Text);
            typeBox.Text = "";
            txtLog_AlignTextToBottom();
            txtLog_ScrollToBottom();
        }

        # endregion

        #region TCPclient tools
        //SENDMESSAGE TO SERVER
        //to send messages to the server use object client. of type TcpClientConnector declared as attribute to send messages to the server
        //usage client.sendmessage(packet);

        //READMESSAGE
        //use client of type TcpClientConnector defined as attribute.
        //usage client.ReadMessage()

        private void on_Window_Closed_Event(object sender, FormClosingEventArgs e)
        {
            Packet p = new Packet(ID, "Disconnect", _defaultDestination, "Disconnecting");
            //send message to server that ur dying
            if (client.isConnected())
            {
                client.sendMessage(p);
                Packet p1 = client.ReadMessage();
                if (p1._message.Equals("LOGGED OFF") && (p1.GetDestination() == "52323232"))
                {
                    client.Close();
                }
                else
                {
                    e.Cancel = true;
                    //show popup that said no response from server (or not). Maybe u shoudnt even cancel the closing operation...
                }
            }
        }

        # endregion

        #region login

        private void on_username_box_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Username_Box.Focused)
            {
                this.ActiveControl = Password_Box;
            }
        }

        private void on_password_box_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Password_Box.Focused)
            {
                login(sender, e); // pass it on to the follow up event
            }
        }

        /**
         * TODO: Logging in for real on the server
         */
        private void login(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Username_Box.Text) || String.IsNullOrEmpty(Password_Box.Text))
            {
                Login_ERROR_Label.Text = "One or more fields are blank!";
                labelRemoveTimer.Start();
                this.ActiveControl = Username_Box;
            }
            else
            {
                string value = Username_Box.Text.Substring(0, 1);
                int id;
                bool isNum = int.TryParse(value, out id);
                Regex r = new Regex(@"^[0-9]{8}$");
                if ((!isNum) || (id < 1) || (id > 8) || (!r.IsMatch(Username_Box.Text)))
                {
                    Login_ERROR_Label.Text = "Client ID must start with 1-8 and is 8 digits long!";
                    labelRemoveTimer.Start();
                    this.ActiveControl = Username_Box;
                }
                //TODO: else if (logins are correct), ipv else (denk ik)
                else
                {
                    ID = Username_Box.Text;
                    setVisibility(true);
                    updateDataTimer.Start(); // automatisch updaten van de waardes
                }
            }
        }
        private void setVisibility(bool v)
        {
            graph.SetVisibibility(v);
            label1.Visible = v;
            TimeRunning_Box.Visible = v;
            label2.Visible = v;
            label3.Visible = v;
            label4.Visible = v;
            label5.Visible = v;
            label6.Visible = v;
            label7.Visible = v;
            RPM_Box.Visible = v;
            Heartbeats_Box.Visible = v;
            Energy_Box.Visible = v;
            Power_Box.Visible = v;
            Brake_Box.Visible = v;
            Distance_Box.Visible = v;
            Speed_Box.Visible = v;
            label8.Visible = v;
            SendMessage.Visible = v;
            typeBox.Visible = v;
            txtLog.Visible = v;
            listView1.Visible = v;

            Password_Box.Visible = !v;
            Username_Box.Visible = !v;
            Password_Label.Visible = !v;
            Username_label.Visible = !v;
            LoginButton.Visible = !v;
            Login_ERROR_Label.Visible = !v;
            if (!v)
            {
                this.ActiveControl = Username_Box;
            }
            else
            {
                this.ActiveControl = typeBox;
            }

        }
        private void UpdateLabel(object sender, EventArgs e)
        {
            Login_ERROR_Label.Text = "";
            labelRemoveTimer.Stop();
        }
        #endregion

        # region Graph Datahandlers and EventListeners

        private void AddGraphToForm()
        {
            object[] data = graph.getComponents();
            this.Controls.Add((System.Windows.Forms.DataVisualization.Charting.Chart)data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                this.Controls.Add((System.Windows.Forms.CheckBox)data[i]);
            }
        }

        #endregion

    }
}