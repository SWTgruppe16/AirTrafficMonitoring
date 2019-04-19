using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{

    public class SeperationCondition : ICondition
    {
        public DateTime TimeOfOccurance { get; set;  }
        public List<string> InvolvedTagIds{ get; set; }
        public string TypeOfCondition { get => "seperation"; }

        public string ID
        {
            get
            {
                var a = InvolvedTagIds[0];
                var b = InvolvedTagIds[1];
                if (a.CompareTo(b) > 0)
                {
                    var tmp = a;
                    a = b;
                    b = tmp;
                }

                return a + b;
            }
        }

        public SeperationCondition(DateTime datetime, string tag_a, string tag_b)
        {
            InvolvedTagIds = new List<string> { tag_a, tag_b };
            TimeOfOccurance = datetime;
        }
    }

    public class SeperationMonitor : IConditionMonitor
    {
        private const double MinimumDistance = 5000.0; //In meters
        private const int MinimumAltitude = 300; //In meters

        private List<SeperationCondition> currentConditions = new List<SeperationCondition>();


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

        

        public List<SeperationCondition> ListOfConditions(List<Track> tracks)
        {
            var seperationOccuredList = new List<SeperationCondition>();

            for (int i = 0; i < tracks.Count; i++) //For every item in list of tracks:
            {
                Track track_a = tracks[i];

                for (int j = i + 1; j < tracks.Count; j++)
                {
                    Track track_b = tracks[j];

                    //In case of collision course, adds timestamp and tagID to seperationOccuredList. 
                    if (IsToClose(track_a, track_b))
                    {
                        var condetion = new SeperationCondition(track_a.TimeStamp, track_a.TagId, track_b.TagId);
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

        public void FindNewConditions(List<SeperationCondition> incomingConditions)
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

        public void AddNewConditions(List<SeperationCondition> incomingConditions)
        {
            foreach (var calculatedCondition in incomingConditions)
            {
                if (IsCurrentInCondition(calculatedCondition.ID))
                {
                    //Remove old condition
                } 
                // Add new condition
            }

            // Raise event with all current
        }

        public void OnTrackRecieved(object sender, TracksInAirspaceArgs tracksInAirspaceArgs)
        {
            var calculatedConditions = ListOfConditions(tracksInAirspaceArgs.Tracks);

            FindNewConditions(calculatedConditions);

            AddNewConditions(calculatedConditions);

        }

        public event EventHandler<NewConditionArgs> newConditionsEvent = delegate { };
        public event EventHandler<CurrentConditionArgs> currentConditionsEvent = delegate{};
    }
}
