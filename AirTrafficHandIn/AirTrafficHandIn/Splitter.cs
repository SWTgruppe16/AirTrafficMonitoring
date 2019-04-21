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
        private readonly List<Track> tracks = new List<Track>();
        public event EventHandler<NewTrackArgs> NewTracks = delegate { };

        public Splitter() { }

        public void OnTransponderData(object sender, RawTransponderDataEventArgs data)
        {
            // Split tracks
            var tracks = SplitData(data.TransponderData);

            // Put tracks i wrapper
            var newTrackArgs = new NewTrackArgs {
                Tracks = tracks
            };

            // Send Event
            sendEvent(newTrackArgs);
        }

        public List<Track> SplitData(List<string> planeinfos)
        {
            // Console.WriteLine("Nyt Event\n==============================");

            var tracks = new List<Track>();

            foreach (var planeinfo in planeinfos)
            {
                // Convert planeinfo to track
                var track = parsePlaneInfo(planeinfo);

                // Adding track to complete list
                tracks.Add(track);

                // Adding track to event list
                tracks.Add(track);

            }

            return tracks;
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
            if (NewTracks != null)
            {
                NewTracks(this, args);
            }
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
