using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Controller
{
    class Enums
    {
        [Flags]
        public enum BikeCommands
        { 
            RESET = "rs",
            LOCK = "lb",
            STATUS = "st",
            POWER = "pw",
            CONTROLMODE = "cm",
            ID = "id",
            TYPE = "ki",
            SETTIME = "pt",
            SETDISTANCE = "pd",
            SETENERGY = "pe",
            GETDATETIME = "tr"
        };
    }
}
