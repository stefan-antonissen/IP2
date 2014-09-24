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

        /**
         * When a client connects start a thread with that client (not here)
         * 
         * What should happen here is a new entry in the client list should be made so that the clientContainer (see design)
         * updates the screen with the new connected client.
         */
        private void on_client_connect_event() // "Client client" Iets in die trend.
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientTab tab = new clientTab("tab1");
            tab.closeAllButThisButton.Click += new System.EventHandler(On_Tab_Close_All_Event);
            tab.closeButton.Click += new System.EventHandler(On_Tab_Closed_Event);
            this.tabControl1.Controls.Add(tab);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientTab tab = new clientTab("tab2");
            tab.closeAllButThisButton.Click += new System.EventHandler(On_Tab_Close_All_Event);
            tab.closeButton.Click += new System.EventHandler(On_Tab_Closed_Event);
            this.tabControl1.Controls.Add(tab);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clientTab tab = new clientTab("tab3");
            tab.closeAllButThisButton.Click += new System.EventHandler(On_Tab_Close_All_Event);
            tab.closeButton.Click += new System.EventHandler(On_Tab_Closed_Event);
            this.tabControl1.Controls.Add(tab);
        }

        # region TabControl Event Handlers
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
        # endregion

        # region Chat Box
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            //nonedonexD
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
           if (typeBox.Text != "")
            {
                txtLog.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
                typeBox.Text = "";
            }
        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (typeBox.Text != "")
                {
                    txtLog.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
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

    #region Tab generation

    public class clientTab : System.Windows.Forms.TabPage
    {
        public  System.Windows.Forms.Button closeButton = new System.Windows.Forms.Button();
        public  System.Windows.Forms.Button closeAllButThisButton = new System.Windows.Forms.Button();
        private System.Windows.Forms.TextBox chatBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox typeBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Button sendButtonClient = new System.Windows.Forms.Button();

        public clientTab(string tabName) //loads of data etc... (joke)
        {
            //System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();


            //close button
            closeButton.Location = new System.Drawing.Point(20, 20);
            closeButton.Text = "Close";

            //close all but this button
            closeAllButThisButton.Location = new System.Drawing.Point(20, 60);
            closeAllButThisButton.Text = "Close all";

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
            this.Controls.Add(closeButton);
            this.Controls.Add(closeAllButThisButton);
            this.Controls.Add(chatBox);
            this.Controls.Add(typeBox);
            this.Controls.Add(sendButtonClient);

            //set tab Settings
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = tabName;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(1232, 631);
            //this.TabIndex = this.tabControl1.TabCount + 1; NOT NEEDED???
            this.Text = tabName;
            this.UseVisualStyleBackColor = true;
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
                chatBox.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
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
                    chatBox.AppendText(Environment.NewLine + "Me: " + typeBox.Text);
                    chatBox_AlignTextToBottom();
                    chatBox_ScrollToBottom();
                    typeBox.Text = "";
                }
            }
        }

        private void chatBox_AlignTextToBottom()
        {
            int visibleLines = (int)(chatBox.Height / chatBox.Font.GetHeight()) - 1;
            if (visibleLines > chatBox.Lines.Length)
            {
                int emptyLines = (visibleLines - chatBox.Lines.Length);
                for (int i = 0; i < emptyLines; i++)
                {
                    chatBox.Text = (Environment.NewLine + chatBox.Text);
                }
            }
        }

        private void chatBox_ScrollToBottom()
        {
            chatBox.SelectionStart = chatBox.Text.Length;
            chatBox.ScrollToCaret();
        }

        # endregion
    }
# endregion
}
