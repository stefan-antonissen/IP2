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
            System.Windows.Forms.TextBox chatBox = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox typeBox = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Button sendButtonClient = new System.Windows.Forms.Button();

            //close button
            closeButton.Location = new System.Drawing.Point(20, 20);
            closeButton.Text = "Close";
            closeButton.Click += new System.EventHandler(On_Tab_Closed_Event);

            //close all but this button
            closeAllButThisButton.Location = new System.Drawing.Point(20, 60);
            closeAllButThisButton.Text = "Close all";
            closeAllButThisButton.Click += new System.EventHandler(On_Tab_Close_All_Event);

            //ChatBox
            chatBox.AllowDrop = true;
            chatBox.BackColor = System.Drawing.Color.WhiteSmoke;
            chatBox.Location = new System.Drawing.Point(34, 317);
            chatBox.Multiline = true;
            chatBox.Name = "txtLog";
            chatBox.ReadOnly = true;
            chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            chatBox.Size = new System.Drawing.Size(983, 282);
            chatBox.TabIndex = 6;
            chatBox.TextChanged += new System.EventHandler(this.txtLog_TextChanged);

            //TypeBox
            typeBox.Location = new System.Drawing.Point(34, 603);
            typeBox.Name = "typeBox";
            typeBox.Size = new System.Drawing.Size(902, 20);
            typeBox.TabIndex = 9;
            typeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(txtLog_KeyDown);

            //sendButton
            sendButtonClient.Location = new System.Drawing.Point(942, 605);
            sendButtonClient.Name = "SendMessage";
            sendButtonClient.Size = new System.Drawing.Size(75, 23);
            sendButtonClient.TabIndex = 10;
            sendButtonClient.Text = "Send";
            sendButtonClient.UseVisualStyleBackColor = true;
            sendButtonClient.Click += new System.EventHandler(this.sendButton_Click);

            //add components
            tabPage.Controls.Add(closeButton);
            tabPage.Controls.Add(closeAllButThisButton);
            tabPage.Controls.Add(chatBox);
            tabPage.Controls.Add(typeBox);
            tabPage.Controls.Add(sendButtonClient);

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

        # region Chat Box
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            //nonedonexD
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (typeBox.Text != "")
            {
                txtLog.AppendText("" + typeBox.Text + "\n");
                typeBox.Text = "";
            }
        }
        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (typeBox.Text != "")
                {

                    //txtLog.AppendText("");
                    txtLog.AppendText(Environment.NewLine + typeBox.Text);
                    txtLog_AlignTextToBottom();
                    txtLog_ScrollToBottom();
                    typeBox.Text = "";
                }
            }
        }
        private void txtLog_AlignTextToBottom()
        {
            int visibleLines = (int)(txtLog.Height / txtLog.Font.GetHeight()) - 1;
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
# endregion
    }
}
