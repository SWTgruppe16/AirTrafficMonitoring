using System;
using System.Collections.Generic;
using System.IO;
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
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var plane_info in logtrack)
            {
                stringBuilder.AppendLine(
                    $"At time: {plane_info.TimeStamp} the following plane {plane_info.TagId} entered the airspace");
            }
            File.AppendAllText(fileLogPath, stringBuilder.ToString());
        }

        public void LogSeperation(object sender, ICollection<string> logtrack)
        {
            var stringBuilder = new StringBuilder();
            foreach (var seperation_info in logtrack)
            {
                var splitSeperationInfo = seperation_info.Split(';');
                stringBuilder.AppendLine($"At time: {splitSeperationInfo[0]} " +
                                         $"plane 1: {splitSeperationInfo[1]} and plane 2: {splitSeperationInfo[2]} were conflicting");
            }
            File.AppendAllText(fileLogPath, stringBuilder.ToString());

        }

        public void LogTrackData(object sender, ICollection<Track> logtracks)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException logtracks) //IO = Invalid Operation Exception 
            {
                //Used for unit testing.
            }
        }
    }
}
