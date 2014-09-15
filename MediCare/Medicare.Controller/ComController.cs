

abstract class ComController
{
    abstract public void openConnection();
    abstract public void closeConnection();
    abstract public void send(string command);
    abstract public string[] getAvailablePorts();
    abstract public string read();

    abstract public string getPort();
}
