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

namespace MediCare.ArtsClient
{
    public partial class Form1 : Form
    {
        private String[] data = {"1", "2", "3", "4", "5", "6", "7", "8"}; // Put GetStatus method here and loop it so it will update textboxes below

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // wtf
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) // wtf
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e) // needs rename
        {
            //send information back to host controller
        }

        private void textbox9_Click(object Sender, EventArgs e) // needs rename
        {
            NewPower_Box.Text = "";
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("tuktuktuktuk"); // do something usefull here
        }

        private void label10_Click(object sender, EventArgs e) // wtf not used
        {

        }

        private void Update_CheckBox_Click(object sender, EventArgs e) // auto update checkbox
        {
            TimeRunning_Update();
        }

        private void comboBox1_SelectedIndexChange(object sender, EventArgs e)
        {
            MessageBox.Show("" + Comport_ComboBox.SelectedItem);
        }

        // While Program is running keep updating text boxes with new data from data [] rawData

        private void TimeRunning_Update()
        {
                TimeRunning_Box.Text = data[0];   
        }

        private void Speed_Update(object sender, EventArgs e)
        {
            Speed_Box.Text = data[1];
        }

        private void Distance_Update(object sender, EventArgs e)
        {
            Distance_Box.Text = data[2];
        }

        private void Brake_Update(object sender, EventArgs e)
        {
            Brake_Box.Text = data[3];
        }

        private void Power_Update(object sender, EventArgs e)
        {
            Power_Box.Text = data[4];
        }

        private void Energy_Update(object sender, EventArgs e)
        {
            Energy_Box.Text = data[5];
        }

        private void Heartbeats_Update(object sender, EventArgs e)
        {
            Heartbeats_Box.Text = data[6];
        }

        private void RPM_Update(object sender, EventArgs e)
        {
            RPM_Box.Text = data[7];
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }


    }
}
