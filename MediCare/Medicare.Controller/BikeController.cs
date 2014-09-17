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
            if (comPort.Equals(""))
            {
                cc = new SerialController();
                comPort = cc.getPort();
            }
            
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

        public string[] GetPorts()
        {
            try
            {
                return cc.getAvailablePorts();
            }
            catch (Exception e)
            {
                string[] rawArray = null;
                rawArray[0] = e.Message.ToString();
                return rawArray; 
            }
        }

        #region getters

        public string[] GetStatus()
        {
            try
            {
                cc.send(Enums.GetValue(Enums.BikeCommands.STATUS));
                string raw = cc.read();
                if (!raw.ToLower().Contains("err"))
                {
                    string[] rawArray = raw.Split();
                    rawArray[3] = (float.Parse(rawArray[3]) / 10).ToString();
                    rawArray[4] = (float.Parse(rawArray[4])).ToString();
                    return rawArray;
                }
                else
                {
                    string[] rawArray = null;
                    rawArray[0] = "ERROR";
                    return rawArray; 
                }
            }
            catch (Exception e)
            {
                string[] rawArray = null;
                rawArray[0] = e.Message.ToString();
                return rawArray;
            }
        }

        #endregion

        #region setters

        public string ResetBike() 
        {
            try
            {
                cc.send(Enums.GetValue(Enums.BikeCommands.RESET));
                return cc.read();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string[] SetPower(int power)
        {
            try
            {
                cc.send(Enums.GetValue(Enums.BikeCommands.CONTROLMODE));
                cc.read();
                cc.send(Enums.GetValue(Enums.BikeCommands.POWER) + /*" " +*/ power.ToString());
                string raw = cc.read();
                string[] rawArray = raw.Split();
                rawArray[3] = (float.Parse(rawArray[3]) / 10).ToString();
                rawArray[4] = (float.Parse(rawArray[4])).ToString();
                return rawArray;
            }
            catch (Exception e)
            {
                string[] rawArray = null;
                rawArray[0] = e.Message.ToString();
                return rawArray;
            }
        }

        #endregion
    }
}
