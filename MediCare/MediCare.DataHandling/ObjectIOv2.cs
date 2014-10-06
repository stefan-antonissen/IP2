using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace MediCare.DataHandling
{
    public class ObjectIOv2
    {
        private ArrayList measurements { get; set; }
        private string timestamp = DateTime.Now.ToString();

        public ObjectIOv2()
        {
            measurements = new ArrayList();
        }

        public void add(string dataString)
        {
            measurements.Add(new Measurement(dataString));
        }

        /*
         * method that handles encryption of the data and saves it to a file on the server.
         */
        public void saveFile(string fileName)
        {
            Stream fileStream = File.OpenWrite(fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, measurements);
        }
    }
}
