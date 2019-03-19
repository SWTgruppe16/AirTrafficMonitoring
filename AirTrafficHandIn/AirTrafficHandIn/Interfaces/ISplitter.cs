using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficHandIn
{
    interface ISplitter
    {
        void SplitData(object sender, RawTransponderDataEventArgs data);
        event EventHandler<List<Tracks>> SplitDataEventHandler;
    }
}
