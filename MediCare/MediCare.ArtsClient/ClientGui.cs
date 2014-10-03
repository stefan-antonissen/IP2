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

namespace MediCare.ArtsClient
{
    public partial class ClientGui : Form
    {
        private Controller.BikeController c;

        private static string server = "127.0.0.1";
        private static int port = 11000;
        private ClientTcpConnector client;
        string ID;

        private readonly Timer updateDataTimer;
        private readonly Timer labelRemoveTimer;
        private bool[] checkbox_Status = { false, false, false, false, false, false, false, false };
        private System.Windows.Forms.DataVisualization.Charting.Series[] ChartData = new System.Windows.Forms.DataVisualization.Charting.Series[8];

        public ClientGui()
        {
            InitializeComponent();
            InitializeGraph();
            this.FormClosing += on_Window_Closed_Event;
            setVisibility(false);

            // update waarden met de data van de fiets
            updateDataTimer = new Timer();
            updateDataTimer.Interval = 500;
            updateDataTimer.Tick += UpdateGUI;

            // timer voor het verwijderen van de errortekst, 'cosmetisch'
            labelRemoveTimer = new Timer();
            labelRemoveTimer.Interval = 3000;
            labelRemoveTimer.Tick += UpdateGUI;

            Connect("SIM");

            //opzetten tcp connectie
            TcpClient TcpClient = new TcpClient(server, port);
            client = new ClientTcpConnector(TcpClient, server);
        }

        private void Connect(String SelectedPort)
        {
            if (SelectedPort.Equals(""))
            {
                c = new BikeController("");
            }
            else if (SelectedPort.Equals("SIM"))
            {
                c = new BikeController("SIM"); // sim is for testing methods
            }
            else
            {
                c = new BikeController(SelectedPort);
            }
        }

        // onderstaande drie methodes zijn om de waarden in de GUI aan te passen
        private void updateValues(String[] data)
        {
            // TODO: data versturen naar de server
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
            return c.GetStatus();
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
                Packet p = new Packet(ID, "Chat", "93238792", typeBox.Text);
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
                    Packet p = new Packet(ID, "Chat", "93238792", typeBox.Text);
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
            Packet p = new Packet(ID, "Disconnect", "92378733", "Disconnecting");
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
            if (!Password_Box.Text.Equals("") && !Username_Box.Text.Equals(""))
            {
                //if (Username_Box.Text == ??? && Password_Box.Text == ???) {
                ID = Username_Box.Text;
                setVisibility(true);
                updateDataTimer.Start(); // automatisch updaten van de waardes
            }
            else
            {
                Login_ERROR_Label.Text = "Invalid username or password";
                this.ActiveControl = Username_Box;
            }
        }
        private void setVisibility(bool v)
        {
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
            Graph.Visible = v;
            Time_Running_CheckBox.Visible = v;
            Speed_CheckBox.Visible = v;
            Distance_CheckBox.Visible = v;
            Brake_CheckBox.Visible = v;
            Power_CheckBox.Visible = v;
            Energy_CheckBox.Visible = v;
            HeartBeats_CheckBox.Visible = v;
            RPM_CheckBox.Visible = v;



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

        private void on_Time_Running_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[0] = !checkbox_Status[0];
            updateGraph(0);
        }

        private void on_Speed_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[1] = !checkbox_Status[1];
            updateGraph(1);
        }

        private void on_Distance_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[2] = !checkbox_Status[2];
            updateGraph(2);
        }

        private void on_Brake_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[3] = !checkbox_Status[3];
            updateGraph(3);
        }

        private void on_Power_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[4] = !checkbox_Status[4];
            updateGraph(4);
        }

        private void on_Energy_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[5] = !checkbox_Status[5];
            updateGraph(5);
        }

        private void on_HeartBeats_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[6] = !checkbox_Status[6];
            updateGraph(6);
        }

        private void on_RPM_CheckBox_Click(object sender, EventArgs e)
        {
            checkbox_Status[7] = !checkbox_Status[7];
            updateGraph(7);
        }

        private void updateGraph(int box_ID)
        {
            Graph.Series.Clear();
            for (int i = 0; i < checkbox_Status.Length; i++)
            {
                if (checkbox_Status[i])
                {
                    Graph.Series.Add(ChartData[i]);
                }
                else
                {
                    //do nothing
                }
            }
        }

        private void InitializeGraph()
        {
            for (int i = 0; i < ChartData.Length; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series s = new System.Windows.Forms.DataVisualization.Charting.Series();
                s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                switch (i)
                {
                    case 0:
                    s.Color = Color.BurlyWood;
                    s.Name = "Time Running";
                    break;
                    case 1:
                    s.Color = Color.Blue;
                    s.Name = "Speed";
                    break;
                    case 2:
                    s.Color = Color.Cyan;
                    s.Name = "Distance";
                    break;
                    case 3:
                    s.Color = Color.DarkOrange;
                    s.Name = "Brake";
                    break;
                    case 4:
                    s.Color = Color.ForestGreen;
                    s.Name = "Power";
                    break;
                    case 5:
                    s.Color = Color.Gold;
                    s.Name = "Energy";
                    break;
                    case 6:
                    s.Color = Color.Magenta;
                    s.Name = "Heart Beats";
                    break;
                    case 7:
                    s.Color = Color.MistyRose;
                    s.Name = "RPM";
                    break;
                    default:
                    s.Color = Color.Black;
                    s.Name = "ERROR NON EXISTEND ITEM LOADED";
                    break;
                }
                ChartData[i] = s;
            }
        }

        /**************************************\
        * TODO: Implement Small Cashing System *
        \**************************************/
        private void process_Graph_Data(String[] data)
        {
            if (data.Length != 1) // maybe not needed if called from updatevalues
            {
                for (int i = 0; i < data.Length; i++)
                {
                    ChartData[i].Points.Add(int.Parse(data[i]));
                }
            }
        }

        #endregion

    }
}
