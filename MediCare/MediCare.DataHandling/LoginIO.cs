using System;
using System.Collections.Generic;
using System.Linq;
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

        public void add(string name, string password)
        {
            logins.Add(name, password);
        }

        public bool login(string name, string password)
        {
            string ActualPassword;
            return logins.TryGetValue(name, out ActualPassword) &&
                       ActualPassword == password;
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
    }
}
