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

namespace MediCare.ArtsClient
{
    public partial class Form1 : Form
    {
        private Controller.BikeController c;

        public Form1()
        {
            InitializeComponent();
            RunProgram();            
        }

        private void Form1_Load(object sender, EventArgs e) // Loads the windows //true story
        {
            String[] ports = c.GetPorts(); //gets list of all com ports
            Comport_ComboBox.Items.AddRange(ports);
        }

        private void Connect(String SelectedPort)
        {
            c = new BikeController("SIM"); // sim is for testing methods
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" + Update_CheckBox.Checked);
        }

        private void Update_CheckBox_Click(object sender, EventArgs e) // auto update checkbox
        {
            //TimeRunning_Update();
        }

        private void ComportComboBox_SelectedIndexChange(object sender, EventArgs e)
        {
            Connect(Comport_ComboBox.SelectedItem.ToString());
        }

        private void RunProgram()
        {
           // while (true)
           // {
            for (int i = 0; i < 10; i++)
            {
                 if (Update_CheckBox.Checked == false)
                {
                    //Console.WriteLine("dit is update" + i);
                    updateValues(c.GetStatus());
                }
            }
           // }
        }
        private void updateValues(String[] data)
        {
            TimeRunning_Box.Text = data[0];
            Speed_Box.Text = data[1];
            Distance_Box.Text = data[2];
            Brake_Box.Text = data[3];
            Power_Box.Text = data[4];
            Energy_Box.Text = data[5];
            Heartbeats_Box.Text = data[6];
            RPM_Box.Text = data[7];
        }
    }
}
