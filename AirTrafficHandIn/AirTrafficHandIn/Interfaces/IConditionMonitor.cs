using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class NewConditionArgs : EventArgs
    {
        public List<ICondition> Conditions { get; set; }
    }

    public class CurrentConditionArgs : EventArgs
    {
        public List<ICondition> Conditions { get; set; }
    }

    public interface IConditionMonitor
    {
        event EventHandler<NewConditionArgs> NewConditionsEvent;
        event EventHandler<CurrentConditionArgs> CurrentConditionsEvent;
        void OnTrackRecieved(object sender, TracksInAirspaceArgs tracksInAirspaceArgs);
    }
}
