using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficHandIn.Interfaces;

namespace AirTrafficHandIn
{
    class FileLogger : ILogger
    {
        private readonly string fileLogPath;
        private string trackEnteredString = "";
        private string trackSeperation = "";

        public void LogTrackEntered(object sender, ICollection<Track> logtrack)
        {
            throw new NotImplementedException();
        }

        public void LogSeperation(object sender, ICollection<string> logtrack)
        {
            throw new NotImplementedException();
        }

        public void LogTrackData(object sender, ICollection<Track> logtracks)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }
    }
}
