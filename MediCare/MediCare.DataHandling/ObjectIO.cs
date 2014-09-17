using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author: Frank
 * @version: 1.0
 * Every data will be stored here (for now) u can call the last 2 methods to save and load the file.
 * It maybe necessary to write something that will split it up in smaller files etc... just ask me to improve in that case.
 * 
 * Tested fully operational.
 *
 */

namespace MediCare.DataHandling
{
    [Serializable()]
    public class ObjectIO
    {
        public ArrayList measurements;
        Serializer serializer;

        public ObjectIO()
        {
            measurements = new ArrayList();
            serializer = new Serializer();
        }

        public void add(Measurement m)
        {
            measurements.Add(m);
        }

        public void delete(int index)
        {
            measurements.RemoveAt(index);
        }

        //Will be inserted AFTER the given index
        public void insert(int index, Measurement m)
        {
            measurements.Insert(index, m);
        }

        public void empty()
        {
            measurements.Clear();
        }

        // to save diskspace always trim before writing to disk
        public void trim(int size)
        {
            measurements.TrimToSize();
        }

        public int getSize()
        {
            return measurements.Capacity;
        }

        // save the arraylist of measurements to a file
        public void SaveMeasurements(string filename)
        {
            serializer.SerializeObject(filename, measurements);
        }

        // load the arraylist of measurements from a file, returns the arraylist
        public ArrayList LoadMeasurements(string filename) {
            ArrayList result = serializer.DeSerializeObject(filename);

            return result;
        }
    }


}
