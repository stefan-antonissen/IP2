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
        private void newpowerbox_TextChanged(object sender, EventArgs e)
        {
        }

        private void newpowerbox_Click(object sender, EventArgs e)
        {
            newpowerbox.Text = "";
        }

        private void comportbox_Select(object sender, EventArgs e, String item)
        {
            MessageBox.Show(item);
        }
    }
}
