using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCare.Controller
{
    public class BikeController
    {
        private ComController cc;

        public BikeController(string comPort)
        {
            if (comPort.Contains("COM"))
            {
                cc = new SerialController(comPort);
                //Console.WriteLine(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
            }
            else
            {
                cc = new BikeSimulator(comPort); //TODO fix.. does not work.
            }

            cc.openConnection();
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

        public string ResetBike() 
        {
            cc.send(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
            cc.read();
            cc.send(Enums.GetValue(Enums.BikeCommands.RESET));
            return cc.read();
        }

        public string[] SetPower(int power)
        {
            cc.send(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
            cc.read();
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
