using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn.Interfaces
{
    public interface ILogger
    {
        void LogTrackEntered(object sender, ICollection<Track> logtrack);
        void LogSeperation(object sender, ICollection<string> logtrack);
        void LogTrackData(object sender, ICollection<Track> logtracks);

        void clear();
    }
}
