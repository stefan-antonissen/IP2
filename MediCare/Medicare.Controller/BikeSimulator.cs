using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            switch (status)
	        {
                case "st":
                    return "st"; //TODO change
                break;
		        default:
                    return "er"; //TODO change
                break;
	        }
        }

        /*public bool isOpen()
        {
            return _comPort.IsOpen();
        }*/
    }
}
