using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public class InAirspace
    {
        public List<Tracks> inAirspace;
        public List<Tracks> newTrack;

        public void OnTrackRecieved(object sender, NewTrackArgs e)
        {
            newTrack = e.tracks_;
            // InAirspacecontrol()
        }
    }
}
