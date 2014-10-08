using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MediCare.NetworkLibrary;

namespace MediCare.DataHandling
{
    public class ObjectIOv2
    {
        private const string _dir = @"Measurements\";
        private const string _fileExt = ".dat";
        public string Status { get;  private set; }
        private Dictionary<string,string> _dirDictionary;
        private const string EncryptionKey = "X10j6CZgLK24OESeXAoq";
        private Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

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
                
                byte[] clearBytes = Encoding.Unicode.GetBytes(p._message);
                string encryptedData = "";
                using (Aes encryptor = Aes.Create())
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptedData = Convert.ToBase64String(ms.ToArray());
                    }
                }
                sw.WriteLine(encryptedData);
            }
        }

        public void Remove_client(Packet p)
        {
            if (_dirDictionary.ContainsKey(p._id))
            {
                _dirDictionary.Remove(p._id);
            }
        }

        public Packet Get_Files(Packet p)
        {
            if (Directory.Exists(_dir))
            {
                foreach (var file in Directory.GetFiles(_dir))
                {
                    if (file.Equals(p._message))
                    {
                        Packet responsePacket = new Packet("server", "Filelist", p._id, string.Join(" ", Directory.GetFiles(file).ToString()));
                        return responsePacket;
                    }
                }
            }
            return new Packet("server", "Filelist", p._id, "No files found");
        }

        public ArrayList Read_file(Packet p)
        {
            string[] lines = File.ReadAllLines(Path.Combine(_dir,p._id,_dirDictionary[p._id] + _fileExt));
            foreach (var line in lines)
            {
                Console.WriteLine(Decrypt(line));
            }
            return null;
        }

        private string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
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
