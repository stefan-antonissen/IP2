using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void writeToDisk()
        {
            serializer.SerializeObject("UrFile.Dat", measurements);
        }

        public ArrayList ReadFromDisk() {
            ArrayList result = serializer.DeSerializeObject("UrFile.Dat");

            return result;
        }
    }


}
