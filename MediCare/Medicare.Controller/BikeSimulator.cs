using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MediCare.Controller
{
    class BikeSimulator : ComController
    {
        private String status = "default";

        public BikeSimulator(string port)
        {
            Console.WriteLine("PortName: " + port);
        }

        override public void openConnection()
        {
            status = "open";
        }

        override public void closeConnection()
        {
            status = "closed";
        }

        override public void send(string command)
        {
            if(Enums.ContainsCommand(command))
	        {
		        status = command;
	        }
        }

        override public string[] getAvailablePorts()
        {
            return null;
        }

        override public string read()
        {
            Thread.Sleep(400);
            switch (status)
            {
                case "st":
                    return "110 40 33 14 400 1200 12 150"; // Heartrate, Rpm, Speed, Distance, Power, Energy, Time, Current Power
                    break;
                default:
                    return "er"; //TODO change
                    break;
            }            
        }

        public override string getPort()
        {
            return "SIM";
        }

        /*public bool isOpen()
        {
            return _comPort.IsOpen();
        }*/
    }
}
