using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCare.DataHandling;
using System.Collections;

namespace MediCare.Server
{
    public class FileIOTestUnit
    {
        ObjectIO o = new ObjectIO();

        public FileIOTestUnit()
        {
            Measurement m = new Measurement(1, 2, 3, 4, 5, 6, 7, 8);
            Measurement m1 = new Measurement(1, 2, 3, 4, 5, 6, 7, 9);
            Measurement m2 = new Measurement(1, 2, 3, 4, 5, 6, 7, 7);
            o.add(m);
            o.add(m1);
            o.add(m2);

            o.SaveMeasurements("filename.dat");
            ArrayList list = o.LoadMeasurements("filename.dat");
            Console.WriteLine(list.ToString());

        }

        static void main(String[] args)
        {
            FileIOTestUnit test = new FileIOTestUnit();

        }
    }
}
