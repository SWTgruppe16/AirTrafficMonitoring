using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTrafficHandIn
{

    public class SeparationMonitor : IConditionMonitor
    {
        private const double MinimumDistance = 5000.0; //In meters
        private const int MinimumAltitude = 300; //In meters
        public event EventHandler<NewConditionArgs> newConditionsEvent;
        public event EventHandler<CurrentConditionArgs> currentConditionsEvent;
        private List<SeparationCondition> currentConditions = new List<SeparationCondition>();


        public bool IsToClose(Track A, Track B)
        {
            //In case of collision course, adds timestamp and tagID to seperationOccuredList. 
            if (Math.Abs(A.Altitude - B.Altitude) < MinimumAltitude &&
                Math.Sqrt(Math.Pow(A.X - B.X, 2) +
                          Math.Pow(A.Y - B.Y, 2)) < MinimumDistance)
            {
                return true;
            }

            return false;
        }

        

        public List<SeparationCondition> ListOfConditions(List<Track> tracks)
        {
            var seperationOccuredList = new List<SeparationCondition>();

            for (int i = 0; i < tracks.Count; i++) //For every item in list of tracks:
            {
                Track track_a = tracks[i];

                for (int j = i + 1; j < tracks.Count; j++)
                {
                    Track track_b = tracks[j];

                    //In case of collision course, adds timestamp and tagID to seperationOccuredList. 
                    if (IsToClose(track_a, track_b))
                    {
                        var condetion = new SeparationCondition(track_a.TimeStamp, track_a.TagId, track_b.TagId);
                        seperationOccuredList.Add(condetion);
                    }    
                }
            }

            return seperationOccuredList;
        }

        public bool IsCurrentInCondition(string ID)
        {
            foreach (var currentCondition in currentConditions)
            {
                if (currentCondition.ID == ID)
                {
                    return true;
                }
            }

            return false;
        }

        public void FindNewConditions(List<SeparationCondition> incomingConditions)
        {
            var newConditions = new NewConditionArgs { Conditions = new List<ICondition>() };
            foreach (var calculatedCondition in incomingConditions)
            {
                if (!IsCurrentInCondition(calculatedCondition.ID))
                {
                    newConditions.Conditions.Add(calculatedCondition);
                }
            }

            if (newConditions.Conditions.Count > 0)
            {
                newConditionsEvent(this, newConditions);
            }
        }

        public void AddNewConditions(List<SeparationCondition> incomingConditions)
        {
            foreach (var calculatedCondition in incomingConditions)
            {
                if (IsCurrentInCondition(calculatedCondition.ID))
                {
                    currentConditions.Remove(calculatedCondition);
                } 
                
                currentConditions.Add(calculatedCondition);
            }

            // Raise event with all current
        }

        public void OnTrackRecieved(object sender, TracksInAirspaceArgs tracksInAirspaceArgs)
        {
            var calculatedConditions = ListOfConditions(tracksInAirspaceArgs.Tracks);

            FindNewConditions(calculatedConditions);

            AddNewConditions(calculatedConditions);

        }

        
        
    }
}
