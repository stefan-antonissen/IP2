using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MediCare.Controller;
using System.IO.Ports;

namespace MediCare.ArtsClient
{
    public partial class ClientGui : Form
    {
        private Controller.BikeController c;
        private string currentPort = "";
        private bool _autoUpdate = false;
        private readonly System.Windows.Forms.Timer _timer;

        public ClientGui()
        {
            InitializeComponent();
            //Connect("");
            _timer = new System.Windows.Forms.Timer
            {
                Interval = 500 // 0.5 delay voor het updaten van de waarden, eventueel nog aanpassen
            };
            _timer.Tick += UpdateGUI;       
        }

        private void Form1_Load(object sender, EventArgs e) // Loads the windows //true story
        {
            //String[] ports = c.GetPorts(); //gets list of all com ports
            Comport_ComboBox.Items.AddRange(SerialPort.GetPortNames());
            Comport_ComboBox.Items.Add("SIM");
        }

        private void Connect(String SelectedPort)
        {
            if (SelectedPort == "")
            {
                c = new BikeController("SIM"); // sim is for testing methods
            }
            else
            {
                c = new BikeController(SelectedPort);
            }
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {

        }
        private void Update_CheckBox_Click(object sender, EventArgs e)
        {
            // auto update werkt
            if (_autoUpdate == false)
            {
                _timer.Start();
                _autoUpdate = !_autoUpdate;
            }
            else if (_autoUpdate == true)
            {
                _timer.Stop();
                _autoUpdate = !_autoUpdate;
            }
        }
        private void ComportComboBox_SelectedIndexChange(object sender, EventArgs e)
        {
            string selectedPort = Comport_ComboBox.SelectedItem.ToString();
            if (currentPort.Equals(""))
            {
                currentPort = selectedPort;
                Connect(selectedPort);
                updateValues(c.GetStatus());
            }
            else if (!selectedPort.Equals(currentPort))
            {
                Connect(selectedPort);
                updateValues(c.GetStatus());
            }
        }

        private void updateValues(String[] data)
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
        private async void UpdateGUI(object sender, EventArgs e)
        {
            var result = await DoWorkAsync();
            updateValues(result);
        }

        private async Task<string[]> DoWorkAsync()
        {
            await Task.Delay(100); // 0.1 delay voor het starten van de update, ook eventueel nog aanpassen
            // hieronder test code om de update te testen, deze methode moet de string array returnen van c.getStatus()
            Random r = new Random();
            string num = r.Next(1, 100).ToString();
            string[] str = new string[] { "1", "2", "3", "4", "5", "6", "7", num };
            return str;
            //return c.getStatus();
        }
    }
}
