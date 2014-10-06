using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.DataHandling
{
    public class ObjectIOv2
    {
        private ArrayList measurements { get; set; };
        private string timestamp = DateTime.Now.ToString();

        public ObjectIOv2()
        {
            measurements = new ArrayList();
        }

        public void add(Measurement m)
        {
            measurements.Add(m);
        }
    }
}
