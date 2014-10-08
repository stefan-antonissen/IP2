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
        public string Status { get;  private set; }
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
            Console.WriteLine("\nTimestamp: " + p._message);

            string _fulldir = "";
            Status = "busy";
            Console.WriteLine(Path.Combine(_dir, p._id));

            try
            {
                if (!Directory.Exists(_dir))
                {
                    Directory.CreateDirectory(_dir);
                    Status = "Creating main directory: " + _dir;
                }
                if (!Directory.Exists(Path.Combine(_dir, p._id)))
                {
                    Directory.CreateDirectory(Path.Combine(_dir, p._id));
                    Status = "creating subdirectory: " + _dir + p._id;
                    Console.WriteLine(Status);
                }
                if (File.Exists(Path.Combine(_dir, p._id, p._message + _fileExt)))
                {
                    Status = "File Already exists: " + Path.Combine(_dir, p._id, p._message + _fileExt);
                    Console.WriteLine(Status);
                    _fulldir = Path.Combine(_dir, p._id, p._message + _fileExt);
                }
                if (!File.Exists(Path.Combine(_dir, p._id, p._message + _fileExt)))
                {
                    Status = "Creating file.." + Path.Combine(_dir, p._id, p._message + _fileExt);
                    File.Create(Path.Combine(_dir, p._id, p._message + _fileExt)).Close();
                    Status = "file Created: " + Path.Combine(_dir, p._id, p._message + _fileExt);
                    _fulldir = Path.Combine(_dir, p._id, p._message + _fileExt);
                    Console.WriteLine(Status);
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
        public void Add_Measurement(Packet p)
        {
            string message = p._message;
            string filename = _dirDictionary[p._id];

            using (StreamWriter sw = File.AppendText(Path.Combine(_dir, p._id, filename + _fileExt)))
            {
                sw.WriteLine(p._message);
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
