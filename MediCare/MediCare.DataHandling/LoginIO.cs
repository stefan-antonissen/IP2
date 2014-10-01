﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.DataHandling
{

    [Serializable()]
    public class LoginIO
    {
        public Dictionary<string, string> logins { get; set; }
        Serializer serializer;

        public LoginIO()
        {
            logins = new Dictionary<string, string>();
            serializer = new Serializer();
        }

        public void add(string credentials)
        {
            string[] splitted = credentials.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            logins.Add(splitted[0], EncryptPassword(splitted[1]));
        }

        public bool login(string name, string password)
        {
            string ActualPassword;
            return logins.TryGetValue(name, out ActualPassword) &&
                       ActualPassword == EncryptPassword(password);
        }

        public void del(string key)
        {
            logins.Remove(key);
        }

        public void empty()
        {
            logins.Clear();
        }

        public int getSize()
        {
            return logins.Count;
        }

        // save the arraylist of measurements to a file
        public void SaveLogins()
        {
            serializer.SerializeObject("USERDATA.STATIC", logins);
        }

        // load the arraylist of measurements from a file, returns the arraylist
        public void LoadLogins()
        {
            this.logins = (Dictionary<string, string>)serializer.DeSerializeObject("USERDATA.STATIC");
        }

        /**
         * Safest way to encrypt data since we dont wanna be able to retrieve the original password.
         * Just run this piece of code to determine IF the password is right or wrong.
         * 
         */
        public static string EncryptPassword(string data)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }
            return data;
        }
    }
}