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
        private bool auto = false;

        public ClientGui()
        {
            InitializeComponent();
            Connect("");
            //RunProgram();            
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

        private void run()
        {
            auto = Update_CheckBox.Checked;
            updateValues(c.GetStatus());
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            updateValues(c.GetStatus());
            auto = Update_CheckBox.Checked;
            while (auto)
            {
                run();
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
    }
}
