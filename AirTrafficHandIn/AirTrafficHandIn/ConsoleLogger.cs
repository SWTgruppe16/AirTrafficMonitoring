using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficHandIn.Interfaces;

namespace AirTrafficHandIn
{
    public class ConsoleLogger : ILogger
    {
        private string trackEnteredString = "";
        private string trackSeperation = "";



        public void LogTrackEntered(object sender, ICollection<Track> logtrack)
        {
            foreach (var plane_info in logtrack)
            {
                trackEnteredString += $"At time: {plane_info.TimeStamp} the following plane {plane_info.TagId} entered the airspace";
            }
        }

        public void LogSeperation(object sender, ICollection<string> logtrack)
        {
            foreach (var seperation_info in logtrack)
            {
                var splitSeperationInfo = seperation_info.Split(';');
                trackSeperation +=
                    $"At time: {splitSeperationInfo[0]} plane 1: {splitSeperationInfo[1]} and plane 2: {splitSeperationInfo[2]} were conflicting";
            }
        }

        public void LogTrackData(object sender, ICollection<Track> logtracks)
        {
            //Kalle hjælp
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
