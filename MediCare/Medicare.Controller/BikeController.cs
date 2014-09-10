using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Controller
{
    public class BikeController
    {
        private ComController cc;
        private int _currentPower;
        public BikeController(string comPort)
        {
            comPort = "COM5"; //TO DO: remove
            cc = new ComController(comPort);
            cc.openConnection();
        }

        public string[] GetStatus()
        {
            cc.send("st"); 
            string raw = cc.read();
            string[] rawArray = raw.Split();
            rawArray[3] = (float.Parse(rawArray[3]) / 10).ToString();
            rawArray[4] = (float.Parse(rawArray[4])).ToString();

            return rawArray;
        }
    }
}
