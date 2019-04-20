using System;
using System.Collections.Generic;

namespace AirTrafficHandIn
{
    public class NewCondition : EventArgs
    {
        public List<SeparationMonitor> NewSeparationConditionList { get; set; }
    }
}
