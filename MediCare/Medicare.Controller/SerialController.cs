using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MediCare.Controller
{
    class SerialController : ComController
    {
        private SerialPort _comPort;

	    public SerialController(string port)
	    {
            _comPort = new SerialPort(port, 9600);
	    }

        override public void openConnection()
        {
            _comPort.Open();
        }

        override public void closeConnection() 
        {
            _comPort.Close();
        }

        override public void send(string command)
        {
            _comPort.WriteLine(command);
        }

        override public string[] getAvailablePorts() 
        {
            return SerialPort.GetPortNames();
        }

        override public string read() 
        {
            return _comPort.ReadLine();
        }

        /*public bool isOpen()
        {
            return _comPort.IsOpen();
        }*/
    }
}
