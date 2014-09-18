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
            this.Update_CheckBox = new System.Windows.Forms.CheckBox();
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
            this.label9 = new System.Windows.Forms.Label();
            this.newPowerBox = new System.Windows.Forms.TextBox();
            this.updatePowerButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.Comport_ComboBox = new System.Windows.Forms.ComboBox();
            this.Comport_ERROR = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time Running";
            // 
            // Update_CheckBox
            // 
            this.Update_CheckBox.AutoSize = true;
            this.Update_CheckBox.Location = new System.Drawing.Point(12, 478);
            this.Update_CheckBox.Name = "Update_CheckBox";
            this.Update_CheckBox.Size = new System.Drawing.Size(86, 17);
            this.Update_CheckBox.TabIndex = 1;
            this.Update_CheckBox.Text = "Auto Update";
            this.Update_CheckBox.UseVisualStyleBackColor = true;
            this.Update_CheckBox.Click += new System.EventHandler(this.Update_CheckBox_Click);
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
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Distance";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Energy";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "Heartbeats";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 35);
            this.label5.TabIndex = 6;
            this.label5.Text = "Power";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 35);
            this.label6.TabIndex = 7;
            this.label6.Text = "Brake";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label8.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 35);
            this.label8.TabIndex = 16;
            this.label8.Text = "RPM";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 407);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 35);
            this.label9.TabIndex = 17;
            this.label9.Text = "New power";
            // 
            // newPowerBox
            // 
            this.newPowerBox.AcceptsReturn = true;
            this.newPowerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPowerBox.Location = new System.Drawing.Point(259, 411);
            this.newPowerBox.Name = "newPowerBox";
            this.newPowerBox.Size = new System.Drawing.Size(178, 35);
            this.newPowerBox.TabIndex = 18;
            this.newPowerBox.Text = "Enter new value";
            this.newPowerBox.GotFocus += new System.EventHandler(this.newPowerBox_GotFocus);
            this.newPowerBox.Leave += new System.EventHandler(this.newPowerBox_Leave);
            // 
            // updatePowerButton
            // 
            this.updatePowerButton.Location = new System.Drawing.Point(443, 411);
            this.updatePowerButton.Name = "updatePowerButton";
            this.updatePowerButton.Size = new System.Drawing.Size(63, 35);
            this.updatePowerButton.TabIndex = 19;
            this.updatePowerButton.Text = "Update";
            this.updatePowerButton.UseVisualStyleBackColor = true;
            this.updatePowerButton.Click += new System.EventHandler(this.updatePowerButton_Click);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(255, 461);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "Com port";
            // 
            // Comport_ComboBox
            // 
            this.Comport_ComboBox.FormattingEnabled = true;
            this.Comport_ComboBox.Location = new System.Drawing.Point(385, 465);
            this.Comport_ComboBox.Name = "Comport_ComboBox";
            this.Comport_ComboBox.Size = new System.Drawing.Size(121, 21);
            this.Comport_ComboBox.TabIndex = 21;
            this.Comport_ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComportComboBox_SelectedIndexChange);
            // 
            // Comport_ERROR
            // 
            this.Comport_ERROR.AutoSize = true;
            this.Comport_ERROR.Location = new System.Drawing.Point(391, 489);
            this.Comport_ERROR.Name = "Comport_ERROR";
            this.Comport_ERROR.Size = new System.Drawing.Size(0, 13);
            this.Comport_ERROR.TabIndex = 22;
            // 
            // ClientGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 507);
            this.Controls.Add(this.Comport_ERROR);
            this.Controls.Add(this.Comport_ComboBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.updatePowerButton);
            this.Controls.Add(this.newPowerBox);
            this.Controls.Add(this.label9);
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
            this.Controls.Add(this.Update_CheckBox);
            this.Controls.Add(this.label1);
            this.Name = "ClientGui";
            this.Text = "MediCare GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Update_CheckBox;
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
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox newPowerBox;
        private System.Windows.Forms.Button updatePowerButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Comport_ComboBox;
        private System.Windows.Forms.Label Comport_ERROR;
    }
}

