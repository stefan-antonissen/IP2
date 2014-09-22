namespace MediCare.ArtsClient
{
    partial class Form1
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
            this.IndexTab = new System.Windows.Forms.TabPage();
            this.SendMessage = new System.Windows.Forms.Button();
            this.typeBox = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.IndexTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // IndexTab
            // 
            this.IndexTab.Controls.Add(this.SendMessage);
            this.IndexTab.Controls.Add(this.typeBox);
            this.IndexTab.Controls.Add(this.txtLog);
            this.IndexTab.Controls.Add(this.listView1);
            this.IndexTab.Controls.Add(this.button3);
            this.IndexTab.Controls.Add(this.button2);
            this.IndexTab.Controls.Add(this.button1);
            this.IndexTab.Controls.Add(this.label1);
            this.IndexTab.Cursor = System.Windows.Forms.Cursors.Default;
            this.IndexTab.Location = new System.Drawing.Point(4, 22);
            this.IndexTab.Name = "IndexTab";
            this.IndexTab.Padding = new System.Windows.Forms.Padding(3);
            this.IndexTab.Size = new System.Drawing.Size(1232, 631);
            this.IndexTab.TabIndex = 0;
            this.IndexTab.Text = "Overview";
            this.IndexTab.UseVisualStyleBackColor = true;
            // 
            // SendMessage
            // 
            this.SendMessage.Location = new System.Drawing.Point(942, 605);
            this.SendMessage.Name = "SendMessage";
            this.SendMessage.Size = new System.Drawing.Size(75, 23);
            this.SendMessage.TabIndex = 10;
            this.SendMessage.Text = "Send";
            this.SendMessage.UseVisualStyleBackColor = true;
            this.SendMessage.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // typeBox
            // 
            this.typeBox.Location = new System.Drawing.Point(34, 603);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(902, 20);
            this.typeBox.TabIndex = 9;
            this.typeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(txtLog_KeyDown);
            // 
            // txtLog
            // 
            this.txtLog.AllowDrop = true;
            this.txtLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLog.Location = new System.Drawing.Point(34, 317);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(983, 282);
            this.txtLog.TabIndex = 6;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(217, 417);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(8, 8);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(34, 204);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 77);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(34, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 77);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 77);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Overview";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.IndexTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 657);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.IndexTab.ResumeLayout(false);
            this.IndexTab.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage IndexTab;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button SendMessage;
        private System.Windows.Forms.TextBox typeBox;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ListView listView1;

    }
}

