using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediCare.Controller;
using MediCare.NetworkLibrary;

namespace MediCare.ArtsClient
{
    static class Program : ClientInterface
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        public void sendPacket()
        {

        }
    }
}

