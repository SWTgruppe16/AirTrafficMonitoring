using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.Core.SequenceChecking;

namespace AirTrafficHandIn
{
    public class TracksInAirspaceArgs : EventArgs
    {
        public List<Track> Tracks { get; set; }
    }

    public class NewTrackInAirSpaceArgs : EventArgs
    {
        public Track Track { get; set; }
    }

    public class TrackLeavesAirSpaceArgs : EventArgs
    {
        public Track Track { get; set; }
    }

    public class AirspaceMonitor
    {
        public readonly List<Track> TracksInAirspace;
        public readonly Airspace Airspace;
        public event EventHandler<TracksInAirspaceArgs> TracksInAirspaceEvent = delegate { };
        public event EventHandler<NewTrackInAirSpaceArgs> NewTrackInTairSpaceEvent = delegate { };
        public event EventHandler<TrackLeavesAirSpaceArgs> TrackLeavesAirSpaceEvent = delegate { };
        public readonly ITrack Tracker;

        public AirspaceMonitor(Airspace airspace, ITrack tracker)
        {
            TracksInAirspace = new List<Track>();
            Airspace = airspace;
            Tracker = tracker;
        }

        public Track GetTrackById(string tag_id)
        {
            foreach (var track in TracksInAirspace)
            {
                if (tag_id == track.TagId)
                {
                    return track;
                }

            }

            return null;
        }

        public void OnTrackRecieved(object sender, NewTrackArgs newTrackArgs)
        {
            
            // Loop through list of tracks
            foreach (var track in newTrackArgs.Tracks)
            {
                // if old in list
                //remove from list
                var old_track = GetTrackById(track.TagId);

                if (old_track != null)
                {
                    TracksInAirspace.Remove(old_track);
                }

                // If not in airspace
                // do nothing
                if (!Airspace.IsInside(track.X, track.Y, track.Altitude))
                {
                    if (old_track != null) {
                        TrackLeavesAirSpaceEvent?.Invoke(this, new TrackLeavesAirSpaceArgs{Track = old_track});
                    }
                    //Console.WriteLine("Not in Airspace: " + track.TagId);

                    continue;
                }

                if (old_track != null)
                {
                    Tracker.calculateCompassCourse(old_track, track);
                    Tracker.calculateSpeed(old_track, track);
                } else {
                    NewTrackInTairSpaceEvent?.Invoke(this, new NewTrackInAirSpaceArgs { Track = track });
                }

                //Console.WriteLine("In Airspace with speed: " + track.TagId + " " + track.Velocity);
                // Verify if in airspace
                // Add new track
                TracksInAirspace.Add(track);


            }

            var tracksInAirSpaceArgs = new TracksInAirspaceArgs {Tracks = TracksInAirspace};
            TracksInAirspaceEvent(this, tracksInAirSpaceArgs);         


        }
    }
}
