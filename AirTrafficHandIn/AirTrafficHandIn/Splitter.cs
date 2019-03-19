using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficHandIn
{
    class Splitter : ISplitter
    {
        public void SplitData(object sender, RawTransponderDataEventArgs data)
        {
            List<Tracks> tracks = new List<Tracks>(); //Create list

            foreach (string planeinfo in data.TransponderData)
            {
                //Use ";" as seperator for splitting data
                var plane_info = planeinfo.Split(';');

                Int32.TryParse(plane_info[1], out var X);
                Int32.TryParse(plane_info[2], out var Y);
                Int32.TryParse(plane_info[3], out var altitude);
                DateTime dateTime;
                dateTime = DateTime.TryParseExact(plane_info[4],
                    "yyyyMMddHHmmssfff",
                    null,
                    DateTimeStyles.None,
                    out dateTime)
                    ? dateTime
                    : DateTime.MinValue;

            }

        }

        public event EventHandler<Tracks> SplitDataEventHandler;
    }
}
