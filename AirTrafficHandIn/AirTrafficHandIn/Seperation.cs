using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class Seperation : IConditions
    {
        private const double MinimumDistance = 5000.0; //In meters
        private const int MinimumAltitude = 300; //In meters

        public List<string> ListOfConditions(List<Tracks> tracks)
        {
            List<string> seperationOccuredList = new List<string>();

            for (int i = 0; i < tracks.Count; i++) //For every item in list of tracks:
            {
                Tracks trackComparisonA = tracks[i];

                for (int j = i + 1; j < tracks.Count; j++)
                {
                    Tracks trackComparisonB = tracks[j];

                    //In case of collision course, adds timestamp and tagID to seperationOccuredList. 
                    if (Math.Abs(trackComparisonA.Altitude - trackComparisonB.Altitude) < MinimumAltitude &&
                        Math.Sqrt(Math.Pow(trackComparisonA.X - trackComparisonB.X, 2) +
                                  Math.Pow(trackComparisonA.Y - trackComparisonB.Y, 2)) < MinimumDistance)
                    {
                        seperationOccuredList.Add($"{trackComparisonA.TimeStamp};{trackComparisonA.TagId};{trackComparisonB.TagId}");
                    }    
                }
            }

            return seperationOccuredList;
        }
    }
}
