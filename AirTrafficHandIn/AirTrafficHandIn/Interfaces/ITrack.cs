using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public interface ITrack
    {
        void calculateSpeed(Track A, Track B);

        void calculateCompassCourse(Track A, Track B);
    }
}
