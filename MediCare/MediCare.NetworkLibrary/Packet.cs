﻿using System;
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
            //encoding blablabla
        }

        public Packet()
        {

        }

        public Packet(string id, string type, string message)
            : this(id, type, "", message)
        {
            //encoding blablabla
        }

        public void HandleServer(ServerInterface server)
        {
            //decoding and sending to server
            //server.sendPacket(tcpClient, this);
        }

        public void HandleClient(ClientInterface client)
        {
            //decoding and sending to client
            //client.sendPacket(this);
        }

        public string GetMessage()
        {
            return _message;
        }

        public string GetDestination()
        {
            return _destination;
        }

        public string GetID()
        {
            return _id;
        }

        public String GetDataString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public string toString()
        {
            return "ID: " + _id + " Type: " + _type + " Destination: " + _destination + " \nMessage: " + _message; 
        }

        static public Boolean hasValidId(Packet p)
        {
            if(p._id.Length == 8)
            {
                int n;
                return int.TryParse(p._id, out n); 
            }
            else if(p._id.Length == 9)
            {
                if (!p._id.EndsWith("r"))
                    return false;

                int n;
                return int.TryParse(p._id.Substring(0, 8), out n); 
            }
            else
            {
                return false;
            }
        }
    }
}
