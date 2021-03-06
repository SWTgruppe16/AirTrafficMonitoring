﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficHandIn.Interfaces
{
    public interface ISplitter
    {
        void OnTransponderData(object sender, RawTransponderDataEventArgs data);
        event EventHandler<NewTrackArgs> NewTracks;
    }
}
