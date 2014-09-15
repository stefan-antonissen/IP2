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
        private Controller.BikeController c = new BikeController("blabla");

        public Form1()
        {
            InitializeComponent();
            RunProgram();            
        }

        private void Form1_Load(object sender, EventArgs e) // Loads the windows //true story
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
            MessageBox.Show("" + Update_CheckBox.Checked);
        }

        private void label10_Click(object sender, EventArgs e) // wtf not used
        {

        }

        private void Update_CheckBox_Click(object sender, EventArgs e) // auto update checkbox
        {
            //TimeRunning_Update();
        }

        private void comboBox1_SelectedIndexChange(object sender, EventArgs e)
        {
            MessageBox.Show("" + Comport_ComboBox.SelectedItem);
        }

        private void RunProgram()
        {
           // while (true)
           // {
            for (int i = 0; i < 10; i++)
            {
                 if (Update_CheckBox.Checked == false)
                {
                    Console.WriteLine("dit is update" + i);
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