using System;
using System.IO.Ports;

public class ComController
{
    private SerialPort _comPort;

	public ComController(string port)
	{
        _comPort = new SerialPort(port);
	}

    public void openConnection()
    {
        _comPort.Open();
    }

    public void closeConnection() 
    {
        _comPort.Close();
    }

    public void send(string command)
    {
        _comPort.Write(command);
    }

    public string[] getAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    public string read() 
    {
        return _comPort.ReadExisting();
    }
}
