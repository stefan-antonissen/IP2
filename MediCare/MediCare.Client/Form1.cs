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
        public System.Windows.Forms.Button closeButton = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button closeAllButThisButton = new System.Windows.Forms.Button();
        private System.Windows.Forms.TextBox chatBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox typeBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Button sendButtonClient = new System.Windows.Forms.Button();

        private System.Windows.Forms.Button updatePowerButton = new System.Windows.Forms.Button();
        public System.Windows.Forms.TextBox newPowerBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label newPowerLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label RPMLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox Speed_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox Distance_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox Brake_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox Power_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox Energy_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox Heartbeats_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox RPM_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label speedLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label brakeLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label powerLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label heartBeatsLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label energyLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label distanceLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.TextBox TimeRunning_Box = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Label timeRunningLabel = new System.Windows.Forms.Label();

        public clientTab(string tabName) //loads of data etc... (joke)
        {
            //System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();


            //close button
            closeButton.Location = new System.Drawing.Point(1070, 600);
            closeButton.Text = "Close";

            //close all but this button
            closeAllButThisButton.Location = new System.Drawing.Point(1150, 600);
            closeAllButThisButton.Text = "Close all";

            //ChatBox
            chatBox.AllowDrop = true;
            chatBox.BackColor = System.Drawing.Color.WhiteSmoke;
            chatBox.Location = new System.Drawing.Point(20, 350);
            chatBox.Multiline = true;
            chatBox.Name = "txtLog";
            chatBox.ReadOnly = true;
            chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            chatBox.Size = new System.Drawing.Size(980, 250);
            chatBox.TabIndex = 6;
            chatBox.TextChanged += new System.EventHandler(this.txtLog_TextChanged);

            //TypeBox
            typeBox.Location = new System.Drawing.Point(20, 603);
            typeBox.Name = "typeBox";
            typeBox.Size = new System.Drawing.Size(902, 20);
            typeBox.TabIndex = 9;
            typeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(txtLog_KeyDown);

            //sendButton
            sendButtonClient.Location = new System.Drawing.Point(928, 600);
            sendButtonClient.Name = "SendMessage";
            sendButtonClient.Size = new System.Drawing.Size(75, 23);
            sendButtonClient.TabIndex = 10;
            sendButtonClient.Text = "Send";
            sendButtonClient.UseVisualStyleBackColor = true;
            sendButtonClient.Click += new System.EventHandler(this.sendButton_Click);

            // 
            // updatePowerButton
            // 
            updatePowerButton.Location = new System.Drawing.Point(760, 437);
            updatePowerButton.Name = "updatePowerButton";
            updatePowerButton.Size = new System.Drawing.Size(63, 35);
            updatePowerButton.TabIndex = 38;
            updatePowerButton.Text = "Update";
            updatePowerButton.UseVisualStyleBackColor = true;
            // 
            // TIME
            // 
            TimeRunning_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            TimeRunning_Box.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            TimeRunning_Box.Location = new System.Drawing.Point(220, 20);
            TimeRunning_Box.Name = "TimeRunning_Box";
            TimeRunning_Box.ReadOnly = true;
            TimeRunning_Box.Size = new System.Drawing.Size(200, 30);
            TimeRunning_Box.TabIndex = 21;
            TimeRunning_Box.TabStop = false;

            timeRunningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            timeRunningLabel.Location = new System.Drawing.Point(20, 20);
            timeRunningLabel.Name = "label1";
            timeRunningLabel.Size = new System.Drawing.Size(200, 30);
            timeRunningLabel.TabIndex = 20;
            timeRunningLabel.Text = "Time Running";
            // 
            // SPEED
            // 
            speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            speedLabel.Location = new System.Drawing.Point(20, 55);
            speedLabel.Name = "label7";
            speedLabel.Size = new System.Drawing.Size(200, 30);
            speedLabel.TabIndex = 27;
            speedLabel.Text = "Speed";

            Speed_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Speed_Box.Location = new System.Drawing.Point(220, 55);
            Speed_Box.Name = "Speed_Box";
            Speed_Box.ReadOnly = true;
            Speed_Box.Size = new System.Drawing.Size(200, 30);
            Speed_Box.TabIndex = 34;

            // 
            // DISTANCE
            // 
            distanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            distanceLabel.Location = new System.Drawing.Point(20, 90);
            distanceLabel.Name = "label2";
            distanceLabel.Size = new System.Drawing.Size(200, 30);
            distanceLabel.TabIndex = 22;
            distanceLabel.Text = "Distance";

            Distance_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Distance_Box.Location = new System.Drawing.Point(220, 90);
            Distance_Box.Name = "Distance_Box";
            Distance_Box.ReadOnly = true;
            Distance_Box.Size = new System.Drawing.Size(200, 30);
            Distance_Box.TabIndex = 33;

            //
            // BRAKE
            //
            brakeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            brakeLabel.Location = new System.Drawing.Point(20, 125);
            brakeLabel.Name = "label6";
            brakeLabel.Size = new System.Drawing.Size(200, 30);
            brakeLabel.TabIndex = 26;
            brakeLabel.Text = "Brake";

            Brake_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Brake_Box.Location = new System.Drawing.Point(220, 125);
            Brake_Box.Name = "Brake_Box";
            Brake_Box.ReadOnly = true;
            Brake_Box.Size = new System.Drawing.Size(200, 30);
            Brake_Box.TabIndex = 32;

            // 
            // POWER
            // 


            Power_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Power_Box.Location = new System.Drawing.Point(220, 160);
            Power_Box.Name = "Power_Box";
            Power_Box.ReadOnly = true;
            Power_Box.Size = new System.Drawing.Size(200, 30);
            Power_Box.TabIndex = 31;

            powerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            powerLabel.Location = new System.Drawing.Point(20, 160);
            powerLabel.Name = "label5";
            powerLabel.Size = new System.Drawing.Size(200, 30);
            powerLabel.TabIndex = 25;
            powerLabel.Text = "Power";
            //
            //   ENERGY
            //
            Energy_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Energy_Box.Location = new System.Drawing.Point(220, 195);
            Energy_Box.Name = "Energy_Box";
            Energy_Box.ReadOnly = true;
            Energy_Box.Size = new System.Drawing.Size(200, 30);
            Energy_Box.TabIndex = 30;

            energyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            energyLabel.Location = new System.Drawing.Point(20, 195);
            energyLabel.Name = "label3";
            energyLabel.Size = new System.Drawing.Size(200, 30);
            energyLabel.TabIndex = 23;
            energyLabel.Text = "Energy";

            // 
            // HEARTBEATS
            // 
            heartBeatsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            heartBeatsLabel.Location = new System.Drawing.Point(20, 230);
            heartBeatsLabel.Name = "label4";
            heartBeatsLabel.Size = new System.Drawing.Size(200, 30);
            heartBeatsLabel.TabIndex = 24;
            heartBeatsLabel.Text = "Heartbeats";

            Heartbeats_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Heartbeats_Box.Location = new System.Drawing.Point(220, 230);
            Heartbeats_Box.Name = "Heartbeats_Box";
            Heartbeats_Box.ReadOnly = true;
            Heartbeats_Box.Size = new System.Drawing.Size(200, 30);
            Heartbeats_Box.TabIndex = 29;
            //
            // RPM
            //
            RPM_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            RPM_Box.Location = new System.Drawing.Point(220, 265);
            RPM_Box.Name = "RPM_Box";
            RPM_Box.ReadOnly = true;
            RPM_Box.Size = new System.Drawing.Size(200, 30);
            RPM_Box.TabIndex = 28;

            RPMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            RPMLabel.Location = new System.Drawing.Point(20, 265);
            RPMLabel.Name = "label8";
            RPMLabel.Size = new System.Drawing.Size(200, 30);
            RPMLabel.TabIndex = 35;
            RPMLabel.Text = "RPM";


            // 
            // NEW POERRBOX
            // 
            newPowerBox.AcceptsReturn = true;
            newPowerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newPowerBox.Location = new System.Drawing.Point(220, 310);
            newPowerBox.Name = "newPowerBox";
            newPowerBox.Size = new System.Drawing.Size(200, 30);
            newPowerBox.TabIndex = 37;
            newPowerBox.Text = "Enter new value";
            newPowerBox.GotFocus += new System.EventHandler(this.newPowerBox_GotFocus);
            newPowerBox.Leave += new System.EventHandler(this.newPowerBox_Leave);
            newPowerBox.KeyDown += new System.Windows.Forms.KeyEventHandler(newPowerBox_KeyDown);

            newPowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newPowerLabel.Location = new System.Drawing.Point(20, 310);
            newPowerLabel.Name = "label9";
            newPowerLabel.Size = new System.Drawing.Size(200, 30);
            newPowerLabel.TabIndex = 36;
            newPowerLabel.Text = "New power";



            //add components
            this.Controls.Add(closeButton);
            this.Controls.Add(closeAllButThisButton);
            this.Controls.Add(chatBox);
            this.Controls.Add(typeBox);
            this.Controls.Add(sendButtonClient);
            this.Controls.Add(updatePowerButton);
            this.Controls.Add(newPowerBox);
            this.Controls.Add(newPowerLabel);
            this.Controls.Add(RPMLabel);
            this.Controls.Add(Speed_Box);
            this.Controls.Add(Distance_Box);
            this.Controls.Add(Brake_Box);
            this.Controls.Add(Power_Box);
            this.Controls.Add(Energy_Box);
            this.Controls.Add(Heartbeats_Box);
            this.Controls.Add(RPM_Box);
            this.Controls.Add(speedLabel);
            this.Controls.Add(brakeLabel);
            this.Controls.Add(powerLabel);
            this.Controls.Add(heartBeatsLabel);
            this.Controls.Add(energyLabel);
            this.Controls.Add(distanceLabel);
            this.Controls.Add(TimeRunning_Box);
            this.Controls.Add(timeRunningLabel);

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

        # region Power Box Tools
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

        # endregion

        // 'Actionlistener' voor de new power textbox
        private void newPowerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (newPowerBox.Text != "")
                {
                    newPowerBox.Text = "";
                }
            }
        }
    }
    # endregion
}
