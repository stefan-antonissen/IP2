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
        private String _status = ""; //command that was sent in trough the send method. excluding the value that has been added to the end.
        private int _value = 0; //value that was passed in witht he command via send.

        private int heartrate = 110;
        private int rpm = 40;
        private int speed = 33;
        private int distance = 14;
        private int power = 400;
        private int energy = 1200;
        private int time = 1111; //11minutes 11 seconds???? verify
        private int currentPower = 150;

        public BikeSimulator(string port)
        {
            Console.WriteLine("PortName: " + port);
        }

        override public void openConnection()
        {
            _status = "open";
        }

        override public void closeConnection()
        {
            _status = "closed";
        }

        override public void send(string command)
        {
            if(Enums.ContainsCommand(command))
	        {
                char[8] = command.ToArray();

		        _status = command.Substring(0, 2);
                _value = int.Parse( command.Substring(2,command.Length - 2 )) ;
	        }
        }

        override public string[] getAvailablePorts()
        {
            return new string[] {"SIM"};
        }

        override public string read()
        {
            Thread.Sleep(400);
            switch (_status)
            {
                case "st":
                    _status = "";
                    return heartrate + " " + rpm + " " + speed + " " + distance + " " + power + " " + energy + " " + time + " " + currentPower; // Heartrate, Rpm, Speed, Distance, Power, Energy, Time, Current Power
                case "pd":
                    distance = _value;
                    return heartrate + " " + rpm + " " + speed + " " + distance + " " + power + " " + energy + " " + time + " " + currentPower; // Heartrate, Rpm, Speed, Distance, Power, Energy, Time, Current Power
                default:
                    return ""; //TODO change
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
