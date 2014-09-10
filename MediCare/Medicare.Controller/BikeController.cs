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
        public BikeController(string comPort)
        {
            comPort = "COM5"; //TO DO: remove
            cc = new ComController(comPort);
            cc.openConnection();
            cc.send(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
            //Console.WriteLine(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
        }

        #region getters
        public string[] GetStatus()
        {
            cc.send(Enums.GetValue(Enums.BikeCommands.STATUS)); 
            string raw = cc.read();
            string[] rawArray = raw.Split();
            rawArray[3] = (float.Parse(rawArray[3]) / 10).ToString();
            rawArray[4] = (float.Parse(rawArray[4])).ToString();
            return rawArray;
        }
        #endregion

        #region setters
        public string[] SetPower(int power)
        {
            cc.send(Enums.GetValue(Enums.BikeCommands.POWER) + " " + power.ToString());
            string raw = cc.read();
            string[] rawArray = raw.Split();
            rawArray[3] = (float.Parse(rawArray[3]) / 10).ToString();
            rawArray[4] = (float.Parse(rawArray[4])).ToString();
            return rawArray;
        }
        #endregion
    }
}
