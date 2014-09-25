namespace MediCare.ArtsClient
{
    partial class ClientGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TimeRunning_Box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RPM_Box = new System.Windows.Forms.TextBox();
            this.Heartbeats_Box = new System.Windows.Forms.TextBox();
            this.Energy_Box = new System.Windows.Forms.TextBox();
            this.Power_Box = new System.Windows.Forms.TextBox();
            this.Brake_Box = new System.Windows.Forms.TextBox();
            this.Distance_Box = new System.Windows.Forms.TextBox();
            this.Speed_Box = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SendMessage = new System.Windows.Forms.Button();
            this.typeBox = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time Running";
            // 
            // TimeRunning_Box
            // 
            this.TimeRunning_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRunning_Box.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TimeRunning_Box.Location = new System.Drawing.Point(259, 61);
            this.TimeRunning_Box.Name = "TimeRunning_Box";
            this.TimeRunning_Box.ReadOnly = true;
            this.TimeRunning_Box.Size = new System.Drawing.Size(215, 35);
            this.TimeRunning_Box.TabIndex = 2;
            this.TimeRunning_Box.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Distance";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Energy";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "Heartbeats";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 35);
            this.label5.TabIndex = 6;
            this.label5.Text = "Power";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 35);
            this.label6.TabIndex = 7;
            this.label6.Text = "Brake";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 35);
            this.label7.TabIndex = 8;
            this.label7.Text = "Speed";
            // 
            // RPM_Box
            // 
            this.RPM_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RPM_Box.Location = new System.Drawing.Point(259, 348);
            this.RPM_Box.Name = "RPM_Box";
            this.RPM_Box.ReadOnly = true;
            this.RPM_Box.Size = new System.Drawing.Size(215, 35);
            this.RPM_Box.TabIndex = 9;
            // 
            // Heartbeats_Box
            // 
            this.Heartbeats_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Heartbeats_Box.Location = new System.Drawing.Point(259, 307);
            this.Heartbeats_Box.Name = "Heartbeats_Box";
            this.Heartbeats_Box.ReadOnly = true;
            this.Heartbeats_Box.Size = new System.Drawing.Size(215, 35);
            this.Heartbeats_Box.TabIndex = 10;
            // 
            // Energy_Box
            // 
            this.Energy_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Energy_Box.Location = new System.Drawing.Point(259, 266);
            this.Energy_Box.Name = "Energy_Box";
            this.Energy_Box.ReadOnly = true;
            this.Energy_Box.Size = new System.Drawing.Size(215, 35);
            this.Energy_Box.TabIndex = 11;
            // 
            // Power_Box
            // 
            this.Power_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Power_Box.Location = new System.Drawing.Point(259, 225);
            this.Power_Box.Name = "Power_Box";
            this.Power_Box.ReadOnly = true;
            this.Power_Box.Size = new System.Drawing.Size(215, 35);
            this.Power_Box.TabIndex = 12;
            // 
            // Brake_Box
            // 
            this.Brake_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Brake_Box.Location = new System.Drawing.Point(259, 184);
            this.Brake_Box.Name = "Brake_Box";
            this.Brake_Box.ReadOnly = true;
            this.Brake_Box.Size = new System.Drawing.Size(215, 35);
            this.Brake_Box.TabIndex = 13;
            // 
            // Distance_Box
            // 
            this.Distance_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Distance_Box.Location = new System.Drawing.Point(259, 143);
            this.Distance_Box.Name = "Distance_Box";
            this.Distance_Box.ReadOnly = true;
            this.Distance_Box.Size = new System.Drawing.Size(215, 35);
            this.Distance_Box.TabIndex = 14;
            // 
            // Speed_Box
            // 
            this.Speed_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speed_Box.Location = new System.Drawing.Point(259, 102);
            this.Speed_Box.Name = "Speed_Box";
            this.Speed_Box.ReadOnly = true;
            this.Speed_Box.Size = new System.Drawing.Size(215, 35);
            this.Speed_Box.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 35);
            this.label8.TabIndex = 16;
            this.label8.Text = "RPM";
            // 
            // SendMessage
            // 
            this.SendMessage.Location = new System.Drawing.Point(920, 657);
            this.SendMessage.Name = "SendMessage";
            this.SendMessage.Size = new System.Drawing.Size(75, 21);
            this.SendMessage.TabIndex = 20;
            this.SendMessage.Text = "Send";
            this.SendMessage.UseVisualStyleBackColor = true;
            this.SendMessage.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // typeBox
            // 
            this.typeBox.Location = new System.Drawing.Point(12, 657);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(902, 20);
            this.typeBox.TabIndex = 19;
            this.typeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLog_KeyDown);
            // 
            // txtLog
            // 
            this.txtLog.AllowDrop = true;
            this.txtLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLog.Location = new System.Drawing.Point(12, 404);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(983, 249);
            this.txtLog.TabIndex = 18;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(195, 446);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(8, 33);
            this.listView1.TabIndex = 17;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // ClientGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.SendMessage);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Speed_Box);
            this.Controls.Add(this.Distance_Box);
            this.Controls.Add(this.Brake_Box);
            this.Controls.Add(this.Power_Box);
            this.Controls.Add(this.Energy_Box);
            this.Controls.Add(this.Heartbeats_Box);
            this.Controls.Add(this.RPM_Box);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeRunning_Box);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ClientGui";
            this.Text = "MediCare GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimeRunning_Box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RPM_Box;
        private System.Windows.Forms.TextBox Heartbeats_Box;
        private System.Windows.Forms.TextBox Energy_Box;
        private System.Windows.Forms.TextBox Power_Box;
        private System.Windows.Forms.TextBox Brake_Box;
        private System.Windows.Forms.TextBox Distance_Box;
        private System.Windows.Forms.TextBox Speed_Box;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button SendMessage;
        private System.Windows.Forms.TextBox typeBox;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ListView listView1;
    }
}

