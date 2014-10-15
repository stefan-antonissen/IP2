using System;
using System.Web.Script.Serialization;

namespace MediCare.NetworkLibrary
{
    [Serializable()]
    public class Packet
    {
        public string _id { get; set; }
        public string _type { get; set; }
        public string _destination { get; set; }
        public string _message { get; set; }

        // ID = id van sender; type = type bericht; destination = ID van ontvanger; message = bericht
        public Packet(string id, string type, string destination, string message)
        {
            this._id = id;
            this._type = type;
            this._destination = destination;
            this._message = message;
        }

        public Packet()
        {

        }

        public Packet(string id, string type, string message)
            : this(id, type, "", message)
        {

        }

        public string GetID()
        {
            return _id;
        }

        public string GetDestination()
        {
            return _destination;
        }

        public string GetType()
        {
            return _type;
        }

        public string GetMessage()
        {
            return _message;
        }

        public String GetDataString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public string toString()
        {
            return "ID: " + _id + " Type: " + _type + " Destination: " + _destination + " \nMessage: " + _message;
        }
    }
}
