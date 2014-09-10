using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ComController cc = new ComController("COM5");
            cc.openConnection();
            cc.send("rs");
            Console.WriteLine((cc.read()));
            cc.send("cm");
            Console.WriteLine((cc.read()));
            while (true)
            {
                cc.send("st");
                Console.WriteLine((cc.read()));
               // Console.ReadKey();
            }
        }
    }
}
