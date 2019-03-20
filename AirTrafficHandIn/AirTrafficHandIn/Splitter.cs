using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using AirTrafficHandIn.Interfaces;

namespace AirTrafficHandIn
{
    public class NewTrackArgs : EventArgs
    {
        public List<Track> Tracks { get; set; }
    }

    public class Splitter : ISplitter
    {
        private List<Track> tracks;
        public event EventHandler<NewTrackArgs> newTrack;

        public Splitter(ITransponderReceiver transponderData)
        {
            tracks = new List<Track>(); //Create list
            newTrack = delegate { };
            transponderData.TransponderDataReady += SplitData;
        }

        protected Track parsePlaneInfo(string planeinfo)
        {
            //Use ";" as seperator for splitting data
            var plane_info = planeinfo.Split(';');

            var tagId = plane_info[0];  //Item 1 is the tagid
            var coordinateX = Int32.Parse(plane_info[1]); //Item 2 is the coordinate for 'X'
            var coordinateY = Int32.Parse(plane_info[2]); //Item 3 is the coordinate for 'Y'
            var altitude = Int32.Parse(plane_info[3]); //Item 4 is the altitude
            var dateTime = DateTime.ParseExact(plane_info[4], //Item 5 is the exact time
                "yyyyMMddHHmmssfff",
                null,
                DateTimeStyles.None);

            var track = new Track
            {
                TagId = tagId,
                X = coordinateX,
                Y = coordinateY,
                Altitude = altitude,
                TimeStamp = dateTime
            };

            return track;
        }

        protected void sendEvent(NewTrackArgs args)
        {
            if (newTrack != null)
            {
                newTrack(this, args);
            }
        }

        public void SplitData(object sender, RawTransponderDataEventArgs data)
        {
            // Console.WriteLine("Nyt Event\n==============================");

            var newTrackArgs = new NewTrackArgs {Tracks = new List<Track>()};

            foreach (var planeinfo in data.TransponderData)
            {
                // Convert planeinfo to track
                var track = parsePlaneInfo(planeinfo);

                // Adding track to complete list
                tracks.Add(track);

                // Adding track to event list
                newTrackArgs.Tracks.Add(track);
                
            }

            // Send event with the track
            sendEvent(newTrackArgs);
            

        }

        //protected virtual void OnNewTrackUpdated(List<Track> tracks)
        //{
        //    if (newTrack != null)
        //    {
        //        newTrack(this, new NewTrackArgs(){Tracks = tracks});
        //    }
        //}
        
    }
}
