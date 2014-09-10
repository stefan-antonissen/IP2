using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Controller
{
    class Enums
    {

        public enum BikeCommands
        { 
            [StringValue("rs")]
            RESET,
            [StringValue("lb")]
            LOCK,
            [StringValue("st")]
            STATUS,
            [StringValue("pw")]
            POWER,
            [StringValue("cm")]
            CONTROLMODE,
            [StringValue("id")]
            ID,
            [StringValue("ki")]
            TYPE,
            [StringValue("pt")]
            SETTIME,
            [StringValue("pd")]
            SETDISTANCE,
            [StringValue("pe")]
            SETENERGY,
            [StringValue("tr")]
            GETDATETIME
        };

        public class StringValue : System.Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

        }
    }
}
