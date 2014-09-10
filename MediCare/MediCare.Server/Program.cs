using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicare.Controller;

namespace MediCare.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            BikeController bc = new BikeController("COM5");
            bc.GetStatus();
            /*
            ComController cc = new ComController("COM5");
            cc.openConnection();
            cc.send("rs");
            Console.WriteLine((cc.read()));
            cc.send("cm");
            Console.WriteLine((cc.read()));
            cc.send("pw 400");
            while (true)
            {
                cc.send("st");
                Console.WriteLine((cc.read()));
               // Console.ReadKey();
            }
             * */
        }
    }
}
