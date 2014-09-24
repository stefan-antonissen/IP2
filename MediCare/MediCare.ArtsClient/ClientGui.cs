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
            chart1.Series["RPM"].Points.AddXY(1,3);
            chart1.Series["RPM"].Points.AddXY(1, 6);
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

        private void updatePowerButton_Click(object sender, EventArgs e)
        {
            //TODO: update de power, updatePowerButton.Text
        }

        // onderstaande twee methodes zijn voor het weergeven van de placeholder tekst
        private void newPowerBox_GotFocus(object sender, EventArgs e)
        {
            if (newPowerBox.Text == "Enter new value")
                newPowerBox.Text = "";
        }

        private void newPowerBox_Leave(object sender, EventArgs e)
        {
            if (newPowerBox.Text == "")
            {
                newPowerBox.Text = "Enter new value";
            }
            else
            {
                newPowerBox.Text = newPowerBox.Text;
            }
        }

        // auto update checkbox
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

        // veranderen van de combobox index
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

        // onderstaande drie methodes zijn om de waarden in de GUI aan te passen
        private void updateValues(String[] data)
        {
            // als de lengte van de data array één is (error), dan zet je alles op 0
            if (data.Length == 1)
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
            Random r = new Random();
            string num = r.Next(1, 100).ToString();
            string[] str = new string[] { "1", "2", "3", "4", "5", "6", "7", num };
            return str;
            //return c.GetStatus();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
