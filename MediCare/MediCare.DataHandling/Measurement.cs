using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author: Frank
 * @version: 1.0
 * 
 * Class whick contaisn data about a Measurement.
 * 
 * Tested fully operational.
 *
 */

namespace MediCare.DataHandling
{

    [Serializable()]
    public class Measurement
    {
        private int heartRate { get; set; }
        private int rpm { get; set; }
        private int speed { get; set; }
        private int distance { get; set; }
        private int power { get; set; }
        private int energy { get; set; }
        private int time { get; set; }
        private int currentPower { get; set; }

        public Measurement(int heartRate, int rpm, int speed, int distance, int power, int energy,  int time, int currentPower)
        {
            this.heartRate = heartRate;
            this.rpm = rpm;
            this.speed = speed;
            this.distance = distance;
            this.power = power;
            this.energy = energy;
            this.time = time;
            this.currentPower = currentPower;
        }

        public Measurement()
        {
            this.heartRate = 0;
            this.rpm = 0;
            this.speed = 0;
            this.distance = 0;
            this.power = 0;
            this.energy = 0;
            this.time = 0;
            this.currentPower = 0;
        }

        public void ToConsole()
        {
            Console.WriteLine("" + this.heartRate + " \n" + this.rpm + " \n" + this.speed + " \n" + this.distance + " \n" + this.power + " \n" + this.energy + " \n" + this.time + " \n" + this.currentPower);
        }
    }
}
