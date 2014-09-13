using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCare.Controller;

namespace MediCare.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            BikeController bc = new BikeController("V");
            //bc.ResetBike();
            while (true)
            {
                String[] stat = bc.GetStatus();
                Console.Write("Heartrate: " + stat[(int)Enums.StatusInfo.HEARTRATE]);
                Console.Write(" Power: "+ stat[(int)Enums.StatusInfo.POWER]);
                Console.Write(" Distance: " + stat[(int)Enums.StatusInfo.DISTANCE]);
                Console.WriteLine(" RPM: " + stat[(int)Enums.StatusInfo.RPM]);
            }
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
