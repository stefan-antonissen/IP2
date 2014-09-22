using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare.ArtsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.Add(generateTab("Button1"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.Add(generateTab("Button2"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.Add(generateTab("Button3"));
        }

        private System.Windows.Forms.TabPage generateTab(string tabName) // Some more database values and or locations can be given in here.
        {
            System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();
            System.Windows.Forms.Button closeButton = new System.Windows.Forms.Button();
            System.Windows.Forms.Button closeAllButThisButton = new System.Windows.Forms.Button();

            //close button
            closeButton.Location = new System.Drawing.Point(20, 20);
            closeButton.Text = "Close";
            closeButton.Click += new System.EventHandler(On_Tab_Closed_Event);

            //close all but this button
            closeAllButThisButton.Location = new System.Drawing.Point(20, 60);
            closeAllButThisButton.Text = "Close all";
            closeAllButThisButton.Click += new System.EventHandler(On_Tab_Close_All_Event);

            //add components
            tabPage.Controls.Add(closeButton);
            tabPage.Controls.Add(closeAllButThisButton);

            //set tab Settings
            tabPage.Cursor = System.Windows.Forms.Cursors.Default;
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = tabName;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(1232, 631);
            tabPage.TabIndex = this.tabControl1.TabCount + 1;
            tabPage.Text = tabName;
            tabPage.UseVisualStyleBackColor = true;

            return tabPage;
        }

        private void On_Tab_Closed_Event(Object Sender, EventArgs e)
        {
            this.tabControl1.Controls.RemoveAt(tabControl1.SelectedIndex);
        }

        //Skip zero because that is our main screen. Also dont close the Current Tab
        private void On_Tab_Close_All_Event(Object Sender, EventArgs e)
        {
            foreach (System.Windows.Forms.TabPage tab in this.tabControl1.TabPages)
            {
                if (!tab.Name.Equals("IndexTab"))
                {
                    this.tabControl1.Controls.Remove(tab);
                }
            }
        }
    }
}