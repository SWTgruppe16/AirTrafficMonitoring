using System;
using System.Collections.Generic;

namespace AirTrafficHandIn
{
    public class CurrentConditions : EventArgs
    {
        public List<SeparationMonitor> CurrentSeparationsList { get; set; }
    }
}
