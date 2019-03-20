using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class Track
    {
        public string TagId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            return "{ID: " + TagId + " (" + X + "," + Y + "," + Altitude + ") Speed: " + Velocity + "}";
        }
    }

    
}