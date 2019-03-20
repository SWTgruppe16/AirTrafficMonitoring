using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficHandIn
{
    public class Splitter : ISplitter
    {

        public Splitter(ITransponderReceiver transponderData)
        {
            transponderData.TransponderDataReady += SplitData;
        }

        public event EventHandler<List<Tracks>> SplitDataEventHandler;

        public void SplitData(object sender, RawTransponderDataEventArgs data)
        {
            List<Tracks> tracks = new List<Tracks>(); //Create list

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
            }



        }

        
    }
}
