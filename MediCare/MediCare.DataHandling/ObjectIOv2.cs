using System;
using System.Collections.Generic;
using System.IO;
using MediCare.NetworkLibrary;

namespace MediCare.DataHandling
{
    public class ObjectIOv2
    {
        private const string _dir = @"Measurements\";
        private const string _fileExt = ".dat";
        public string Status { get; set; }
        private Dictionary<string,string> _dirDictionary;

        public ObjectIOv2()
        {
            _dirDictionary = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// This method creates a file from a packet with a timestamp
        /// </summary>
        /// <param name="p">Packet</param>
        /// <returns>Full Directory of created file/existing file</returns>
        public string Create_file(Packet p)
        {
            string _fulldir = "";
            Status = "busy";

            try
            {
                if (!Directory.Exists(_dir))
                {
                    Directory.CreateDirectory(_dir);
                    Status = "Creating main directory: " + _dir;
                }
                if (!Directory.Exists(_dir + p._id))
                {
                    Directory.CreateDirectory(_dir + p._id);
                    Status = "creating subdirectory: " + _dir + p._id;
                }
                if (File.Exists(_dir + p._id + @"\" + p._message + _fileExt))
                {
                    Status = "File Already exists: " + _dir + p._id + @"\" + p._message + _fileExt;
                    _fulldir = _dir + p._id + @"\" + p._message;
                }
                if (!File.Exists(_dir + p._id + @"\" + p._message + _fileExt))
                {
                    File.Create(_dir + p._id + @"\" + p._message + _fileExt);
                    Status = "file Created: " + _dir + p._id + @"\" + p._message + _fileExt;
                    _fulldir = _dir + p._id + @"\" + p._message + _fileExt;
                    _dirDictionary.Add(p._id, p._message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("A FileIO Measurement error occured: " + e.Message);
            }
            return _fulldir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void Add(Packet p)
        {
            string message = p._message;
            string filename = _dirDictionary[p._id];

            using (StreamWriter sw = File.AppendText(filename))
            {

            }	
        }

        public void Remove_client(Packet p)
        {
            if (_dirDictionary.ContainsKey(p._id))
            {
                _dirDictionary.Remove(p._id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void Remove_file(Packet p)
        {
        }
    }
}
