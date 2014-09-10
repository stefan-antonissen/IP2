using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediCare.ArtsClient
{
    static class Program
    {
        private const int HEARTRATE = 0;
        private const int RPM = 1;
        private const int SPEED = 2;
        private const int DISTANCE = 3;
        private const int POWER = 4;
        private const int ENERGY = 5;
        private const int TIME = 6;
        private const int CURRENTPOWER = 7; 
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
    }
}
