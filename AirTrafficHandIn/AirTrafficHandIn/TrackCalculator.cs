using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class TrackCalculator : ITrack
    {
        public void calculateSpeed(Tracks A, Tracks B)
        {
            double deltaXY = Math.Sqrt((Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2)));
            double deltaAltitude = Math.Abs(A.Altitude - B.Altitude);
            double deltaDistance = Math.Sqrt((Math.Pow(deltaXY, 2) + Math.Pow((deltaAltitude), 2)));
            double deltaTime = B.TimeStamp.Subtract(A.TimeStamp).TotalSeconds;

            B.Velocity = (deltaDistance / deltaTime);
        }

        public void calculateCompassCourse(Tracks A, Tracks B)
        {
            double deltaX = A.X - B.X;
            double deltaY = A.Y - B.Y;

            double compassCourse = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

            if (compassCourse < 0)
            {
                compassCourse += 360;
            }

            B.CompassCourse = compassCourse;
        }
    }
}
