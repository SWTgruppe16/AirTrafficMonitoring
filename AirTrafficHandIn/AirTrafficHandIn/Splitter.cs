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
        public List<Tracks> tracks_ { get; set; }
    }

    public class Splitter : ISplitter
    {
        private List<Tracks> tracks; 

        public Splitter(ITransponderReceiver transponderData)
        {
            transponderData.TransponderDataReady += SplitData;
            tracks = new List<Tracks>(); //Create list
        }

        public event EventHandler<NewTrackArgs> newTrack;


        public void SplitData(object sender, RawTransponderDataEventArgs data)
        {
            foreach (string planeinfo in data.TransponderData)
            {
                //Use ";" as seperator for splitting data
                var plane_info = planeinfo.Split(';');

                Int32.TryParse(plane_info[1], out var coordinateX); //Item 2 is the coordinate for 'X'
                Int32.TryParse(plane_info[2], out var coordinateY); //Item 3 is the coordinate for 'Y'
                Int32.TryParse(plane_info[3], out var altitude); //Item 4 is the altitude
                DateTime dateTime;
                dateTime = DateTime.TryParseExact(plane_info[4], //Item 5 is the exact time
                    "yyyyMMddHHmmssfff",
                    null,
                    DateTimeStyles.None,
                    out dateTime)
                    ? dateTime
                    : DateTime.MinValue;

                tracks.Add(new Tracks()
                {
                    TagId = plane_info[0], //Item 1 is the tag ID
                    X = coordinateX,
                    Y = coordinateY,
                    Altitude = altitude,
                    TimeStamp = dateTime,
                });

                OnNewTrackUpdated(tracks);
            }

        }

        protected virtual void OnNewTrackUpdated(List<Tracks> tracks)
        {
            if (newTrack != null)
            {
                newTrack(this, new NewTrackArgs(){tracks_ = tracks});
            }
        }
        
    }
}
