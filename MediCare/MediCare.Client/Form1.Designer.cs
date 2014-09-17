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
            this.components = new System.ComponentModel.Container();
            this.newpowerbox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comlabel = new System.Windows.Forms.Label();
            //this.checkBox1.Location = new System.Drawing.Point(18, 498);
            //this.label9.Location = new System.Drawing.Point(12, 413);
            // newpowerbox
            this.newpowerbox.AcceptsReturn = true;
            this.newpowerbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newpowerbox.Location = new System.Drawing.Point(259, 413);
            this.newpowerbox.Name = "newpowerbox";
            this.newpowerbox.Size = new System.Drawing.Size(180, 35);
            this.newpowerbox.TabIndex = 18;
            this.newpowerbox.Text = "Enter new value";
            this.newpowerbox.Click += new System.EventHandler(this.newpowerbox_Click);
            this.newpowerbox.TextChanged += new System.EventHandler(this.newpowerbox_TextChanged);
            //this.updatebutton.Location = new System.Drawing.Point(445, 413);
            //this.updatebutton.Size = new System.Drawing.Size(58, 35);
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(394, 484);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(109, 21);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.Items.Add("hoi"); // for each loop necessary to add everything to the array
            this.comboBox1.Items.Add("doei");
            this.comboBox1.Items.Add("knappe");
            this.comboBox1.Items.Add("jongen");
            // 
            // comlabel
            // 
            this.comlabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comlabel.Location = new System.Drawing.Point(255, 480);
            this.comlabel.Name = "comlabel";
            this.comlabel.Size = new System.Drawing.Size(78, 31);
            this.comlabel.TabIndex = 21;
            this.comlabel.Text = "Com port";
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 527);
            this.Controls.Add(this.comlabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.newpowerbox);
        }

        #endregion
        public System.Windows.Forms.TextBox newpowerbox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label comlabel;
    }
}

