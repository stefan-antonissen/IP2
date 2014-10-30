﻿using System;
using System.Collections;
using System.Drawing;
using System.Net.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
using Timer = System.Windows.Forms.Timer;

namespace MediCare.Client
{
    public partial class ClientGui : Form
    {
        private Controller.BikeController _bikeController;
        private Graph _graph;
        private Series[] _ChartData = new Series[8];

        private static string _server = NetworkSettings.SERVERIP;
        private static int _port = NetworkSettings.SERVERPORT;
        private ClientTcpConnector _client;

        private const bool SIM_ON = true;

        string _ID;
        private Boolean _userIsAuthenticated = false;

        private readonly Timer _updateDataTimer;
        private readonly Timer _labelRemoveTimer;

        private string _defaultDestination = "Dokter";

        //USE THIS FORMAT WHEN SENDING DATETIME PACKET!
        private string _DateFileFormat = "yyyy_MM_dd HH_mm_ss";

        private bool first = true;
        private bool testBusy = false;

        public ClientGui()
        {
            bool success = false;
            InitializeComponent();
            _graph = new Graph();
            _graph.Initialize_Checkboxes_Client();
            _graph.InitializeChart_Client();
            _graph.InitializeGraph();
            AddGraphToForm();

            this.FormClosing += on_Window_Closed_Event;
            setVisibility(false);

            // update waarden met de data van de fiets
            _updateDataTimer = new Timer();
            _updateDataTimer.Interval = 1000;
            _updateDataTimer.Tick += UpdateGUI;

            // timer voor het verwijderen van de errortekst, 'cosmetisch'
            _labelRemoveTimer = new Timer();
            _labelRemoveTimer.Interval = 3000;
            _labelRemoveTimer.Tick += UpdateLabel;

            if(SIM_ON)
            {
                Connect("SIM");
            }
            else
            {
                Connect("");
            }

            _bikeController.LockPower();

            try
            {
                //opzetten tcp connectie
                TcpClient TcpClient = new TcpClient(_server, _port);
                _client = new ClientTcpConnector(TcpClient, _server);

                new System.Threading.Thread(() =>
                {
                    while (true)
                    {
                        if (_userIsAuthenticated)
                        {
                            Packet packet = null;
                            if (_client.isConnected() && !this.IsDisposed)
                            {
                                //Console.WriteLine("Reading message\n");
                                packet = _client.ReadMessage();

                                if (packet != null)
                                {
                                    processPacket(packet);
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(5);
                    }
                }).Start();
                success = true;

            }
            catch (SocketException)
            {
                MessageBox.Show("Could not connect to the server, trying to reconnect");
                success = false;
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Lost connection to the server (timed out), trying to reconnect");
                success = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured: " + e.Message);
                success = false;
            }
                //rewrite this to make the the reconnection
            finally
            {
                if (!success)
                {
                    //opzetten tcp connectie
                    TcpClient TcpClient = new TcpClient(_server, _port);
                    _client = new ClientTcpConnector(TcpClient, _server);

                    new System.Threading.Thread(() =>
                    {
                        while (true)
                        {
                            if (_userIsAuthenticated)
                            {
                                Packet packet = null;
                                if (_client.isConnected() && !this.IsDisposed)
                                {
                                    //Console.WriteLine("Reading message\n");
                                    packet = _client.ReadMessage();

                                    if (packet != null)
                                    {
                                        processPacket(packet);
                                    }
                                }
                            }
                            System.Threading.Thread.Sleep(5);
                        }
                    }).Start();
                }
            }
        }


        private void processPacket(Packet p)
        {
            //Console.WriteLine("Type: " + p._type);
            //Console.WriteLine("Received packet with message: " + p._message);
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
                case "Disconnect":
                HandleDisconnectPacket(p);
                break;
                case "FirstConnect":
                HandleFirstConnectPacket(p);
                break;
                case "CycleTest":
                HandleStartTestPacket(p);
                break;
                default: //nothing
                break;
            }
        }

        private void handleFinishTestPacket()
        {
            throw new NotImplementedException();
        }

        private void HandleStartTestPacket(Packet p)
        {
            if(!testBusy)
            {
                testBusy = true;
                new System.Threading.Thread(() =>
                {
                    _bikeController.SetPower(50);
                    int time = 0;
                    int testTimeCompleted = 0;
                    ArrayList heartbeatList = new ArrayList();
                    int currentPower = 50;
                    int powerIncrement = 0;
                    bool correctRPM = false;
                    if (p._message == "male")
                    {
                        powerIncrement = 50;
                    }
                    else if (p._message == "female")
                    {
                        powerIncrement = 25;
                    }
                    on_message_receive_event("", "Inspanningstest : " + "Welkom bij de inspanningstest, probeert u een omwentelingen per min van ong. 60 te behouden tijdens de gehele test");
                    while (true)
                    {
                        if (int.Parse(RPM_Box.Text) < 50 && correctRPM && (time % 10 == 0))
                        {
                            on_message_receive_event("", "Please cycle faster to get your rpm up to 60");
                            correctRPM = false;
                        }
                        else if (int.Parse(RPM_Box.Text) > 70 && correctRPM && (time % 10 == 0))
                        {
                            on_message_receive_event("", "Please cycle slower to get your rpm down to 60");
                            correctRPM = false;
                        }
                        else
                        {
                            correctRPM = true;
                        }
                        if (int.Parse(Heartbeats_Box.Text) < 140 && time > 20 && currentPower < 400)
                        {
                            currentPower += powerIncrement;
                            _bikeController.SetPower(currentPower);
                            on_message_receive_event("", "Inspanningstest : " + "Power opgevoerd naar " + currentPower);
                            time = 0;
                        }
                        else if (int.Parse(Heartbeats_Box.Text) > 140)
                        {
                            testTimeCompleted++;
                            if (testTimeCompleted%60 == 0)
                            {
                                on_message_receive_event("", "Heartbeat added");
                                heartbeatList.Add(int.Parse(Heartbeats_Box.Text));
                            }
                            if (testTimeCompleted%360 == 0)
                            {
                                on_message_receive_event("", "Test completed, sending results");
                                //todo send results
                            }
                        }
                        time++;
                        try
                        {
                            Thread.Sleep(1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }).Start();
            }
        }

        private void HandleFirstConnectPacket(Packet p)
        {
            if (p._message == "VERIFIED")
            {
                Console.WriteLine("Succesfully logged in");
            }
            else
            {
                displayErrorMessage(p._message);
            }
        }

        private void HandleDisconnectPacket(Packet p)
        {
            _client.Close();
        }

        private void HandleCommandPacket(Packet p)
        {
            Console.WriteLine("Received Command message: " + p._message);
            int value;
            if (p._message.Equals("reset"))
            {
                _bikeController.ResetBike();
            }
            else if (int.TryParse(p._message, out value))
            {
                _bikeController.SetPower(value);
            }
        }

        private void HandleChatPacket(Packet p)
        {
            on_message_receive_event(p._id, p._message);
        }

        private void Connect(String SelectedPort)
        {
            if (SelectedPort.Equals(""))
            {
                _bikeController = new BikeController("");
            }
            else if (SelectedPort.Equals("SIM"))
            {
                _bikeController = new BikeController("SIM"); // sim is for testing methods
            }
            else
            {
                _bikeController = new BikeController(SelectedPort);
            }
        }

        // onderstaande drie methodes zijn om de waarden in de GUI aan te passen
        private void updateValues(String[] data)
        {
            bool success = false;

            if (first)
            {
                string[] timestamp = DateTime.Now.ToString(_DateFileFormat).Split();
                #region SendTimeStamp
                try
                {
                    SendMeasurementData(timestamp, "Timestamp");
                    success = true;
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("A time-out occured, trying again");
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        SendMeasurementData(timestamp, "Timestamp");
                    }
                    success = false;
                }
                #endregion
                #region sendmessage(packet filelist)

                try
                {
                    _client.sendMessage(new Packet(_ID, "Filelist", "98765432", "12345678"));
                    success = true;
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("an timeout occured, retrying");
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        _client.sendMessage(new Packet(_ID, "Filelist", "98765432", "12345678"));
                    }
                    success = false;
                }

                #endregion
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
                _graph.process_Graph_Data(data);
            }
        }

        //TODO Destination is hard-coded
        private void SendMeasurementData(string[] data, string type)
        {
            bool success = false;
            string s;
            if (data.Length < 8)
            {
                s = data[0] + " " + data[1];
            }
            else
            {
                s = data[0] + " " + data[1] + " " + data[2] + " " + data[3] + " " + data[4] + " " + data[5] + " " + data[6] + " " + data[7];
            }
            Packet p = new Packet(_ID, type, _defaultDestination, s);
            if (_client.isConnected())
            {
                try
                {
                    _client.sendMessage(p);
                    success = true;
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("An timeout occured, retrying");
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        _client.sendMessage(p);
                    }
                    success = false;
                }
            }
        }

        private async void UpdateGUI(object sender, EventArgs e)
        {
            try
            {
                var result = await DoWorkAsync();
                updateValues(result);
            }
            catch
            {
                _updateDataTimer.Stop();
                Form f = new Form();
                f.Text = "Waiting for reconnect...";
                MessageBox.Show("Bike Connection error, please reconnect the bike!");
                    //MessageBox.Show("Bike Connection error, please reconnect the bike!");
                f.Show(this);         //Make sure we're the owner
                this.Enabled = false; //Disable ourselves
                while(!_bikeController.IsConnected())
                {
                    try
                    {
                        Connect("");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                this.Enabled = true;  //We're done, enable ourselves
                f.Close();            //Dispose message form
                MessageBox.Show("Bike Connection restored");
                _updateDataTimer.Start();
            }
        }

        private async Task<string[]> DoWorkAsync()
        {
            await Task.Delay(100); // 0.1s delay voor het starten van de update, ook eventueel nog aanpassen
            // hieronder test code om de update te testen, deze methode moet de string array returnen van c.getStatus()
            //Random r = new Random();
            //string num = r.Next(1, 100).ToString();
            //string[] str = new string[] { num, num, num, num, num, num, num, num };
            // return str;
            string[] data = _bikeController.GetStatus();
            return new string[] { data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7] };
        }

        # region Chat Box
        // ID = id van sender; type = type bericht; destination = ID van ontvanger; message = bericht
        private void sendButton_Click(object sender, EventArgs e)
        {
            bool success = false;
            if (typeBox.Text != "")
            {
                Packet p = new Packet(_ID, "Chat", _defaultDestination, typeBox.Text);
                try
                {
                    _client.sendMessage(p);
                    success = true;
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("An timeout occured, retrying");
                    success = false;
                }
                finally
                {
                    if (!success)
                    {
                        _client.sendMessage(p);
                    }
                    success = false;
                }
                txtLog.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
                txtLog_AlignTextToBottom();
                txtLog_ScrollToBottom();
                typeBox.Text = "";
            }
        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            bool success = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (typeBox.Text != "")
                {
                    Packet p = new Packet(_ID, "Chat", _defaultDestination, typeBox.Text);
                    try
                    {
                        _client.sendMessage(p);
                        success = true;
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show("An timeout occured, retrying");
                        success = false;
                    }
                    finally
                    {
                        if (!success)
                        {
                            _client.sendMessage(p);
                        }
                        success = false;
                    }
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
        delegate void UpdateChat(string identification, string text);

        public void on_message_receive_event(string id, string message)
        {
            if (txtLog.InvokeRequired)
            {
                UpdateChat d = new UpdateChat(on_message_receive_event);
                this.Invoke(d, new object[] { id, message });
            }
            else
            {
                txtLog.AppendText(Environment.NewLine + "Dokter " + id + ": " + message);
                txtLog_AlignTextToBottom();
                txtLog_ScrollToBottom();
            }
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
            bool success = false;
            DialogResult result = MessageBox.Show("Are you sure you want to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Packet p = new Packet(_ID, "Disconnect", _defaultDestination, "Disconnecting");
                //send message to server that ur dying
                if (_client.isConnected())
                {
                    try
                    {
                        _client.sendMessage(p);
                        success = true;
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show("An timeout occured, retrying");
                        success = false;
                    }
                    finally
                    {
                        if (!success)
                        {
                            _client.sendMessage(p);
                        }
                        success = false;
                    }
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

        private void login(object sender, EventArgs e)
        {
            bool success = false;
            if (String.IsNullOrEmpty(Username_Box.Text) || String.IsNullOrEmpty(Password_Box.Text))
            {
                displayErrorMessage("One or more fields are blank!");
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
                    displayErrorMessage("Client ID must start with 1-8 and is 8 digits long!");
                    this.ActiveControl = Username_Box;
                }
                else
                {
                    string tempID = Username_Box.Text;
                    
                    try
                    {
                        _client.sendFirstConnectPacket(tempID, Password_Box.Text);
                        success = true;
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show("An timeout occured, retrying");
                        success = false;
                    }
                    finally
                    {
                        if (!success)
                        {
                            _client.sendFirstConnectPacket(tempID, Password_Box.Text);
                        }
                        success = false;
                    }

                    while (!_userIsAuthenticated)
                    {
                        //pol for packets. if packet == authenticated!

                        Packet packet = null;
                        if (_client.isConnected())
                        {
                            packet = _client.ReadMessage();

                            if (packet._message.Equals("VERIFIED"))
                            {
                                //todo check for authenticated packet from server 
                                _userIsAuthenticated = true;
                                _ID = tempID;

                                setVisibility(true);
                                _updateDataTimer.Start(); // automatisch updaten van de waardes
                            }
                            else
                            {
                                displayErrorMessage(packet._message);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void displayErrorMessage(string message)
        {
            Login_ERROR_Label.Text = message;
            _labelRemoveTimer.Start();
        }

        private void setVisibility(bool v)
        {
            _graph.SetVisibibility(v);
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
            _labelRemoveTimer.Stop();
        }
        #endregion

        # region Graph Datahandlers and EventListeners

        private void AddGraphToForm()
        {
            object[] data = _graph.getComponents();
            this.Controls.Add((System.Windows.Forms.DataVisualization.Charting.Chart)data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                this.Controls.Add((System.Windows.Forms.CheckBox)data[i]);
            }
        }

        #endregion

    }
}