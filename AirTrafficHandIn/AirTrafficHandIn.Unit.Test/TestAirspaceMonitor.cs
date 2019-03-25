using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTrafficHandIn.Unit.Test
{
    [TestFixture]
    class TestAirspaceMonitor
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void AirspaceMonitorEmptyTest()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 10,
                Y = 10,
                Z = 10,
                depth = 1000,
                width = 2000,
                height = 500
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<TracksInAirspaceArgs> tracksInAirspaceHandler = (object sender, TracksInAirspaceArgs e) =>
            {
                int numInList = e.Tracks.Count; // Number of tracks in airspace
                Assert.That(numInList, Is.EqualTo(0));
                results.Add(e.Tracks);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            List<Track> tracksEmptyList = new List<Track>(); // opret liste
            // Evt tilføj ting på listen
            NewTrackArgs newTrack = new NewTrackArgs();  // opret taske
            newTrack.Tracks = tracksEmptyList;  // Putter listen ned i tasken

            uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitorSingleInsideTest()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 10,
                Y = 10,
                Z = 10,
                depth = 1000,
                width = 2000,
                height = 500
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<TracksInAirspaceArgs> tracksInAirspaceHandler = (object sender, TracksInAirspaceArgs e) =>
            {
                int numInList = e.Tracks.Count; // Number of tracks in airspace
                Assert.That(numInList, Is.EqualTo(1));
                results.Add(e.Tracks);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track insideTrack = new Track
            {
                X = 10,
                Y = 10,
                Altitude = 10,
                TagId = "ABC123"
            };

            List<Track> tracksSingleInsideList = new List<Track>
            {
                insideTrack
            }; // opret liste

            NewTrackArgs newTrack = new NewTrackArgs
            {
                Tracks = tracksSingleInsideList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(1));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitorSingleOutsideTest()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 10,
                Y = 10,
                Z = 10,
                depth = 1000,
                width = 2000,
                height = 500
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<TracksInAirspaceArgs> tracksInAirspaceHandler = (object sender, TracksInAirspaceArgs e) =>
            {
                int numInList = e.Tracks.Count; // Number of tracks in airspace
                Assert.That(numInList, Is.EqualTo(0));
                results.Add(e.Tracks);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track insideTrack = new Track
            {
                X = 8,
                Y = 8,
                Altitude = 8,
                TagId = "DEF456"
            };

            List<Track> tracksSingleInsideList = new List<Track>
            {
                insideTrack
            }; // opret liste

            NewTrackArgs newTrack = new NewTrackArgs
            {
                Tracks = tracksSingleInsideList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_OneOutside_OneInside_Airspace_Test()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 10,
                Y = 10,
                Z = 10,
                depth = 1000,
                width = 2000,
                height = 500
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>(); //Antal lister modtaget med tracks

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<TracksInAirspaceArgs> tracksInAirspaceHandler = (object sender, TracksInAirspaceArgs e) =>
            {
                int numInList = e.Tracks.Count; // Number of tracks in airspace
                Assert.That(numInList, Is.EqualTo(1));
                results.Add(e.Tracks);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track insideTrack = new Track
            {
                X = 15,
                Y = 15,
                Altitude = 15,
                TagId = "DEF456"
            };

            List<Track> tracksSingleInsideList = new List<Track>
            {
                insideTrack
            }; // opret liste


            Track outsideTrack = new Track
            {
                X = 8,
                Y = 8,
                Altitude = 8,
                TagId = "AAA111"
            };

            List<Track> trackSingleOutsideList = new List<Track>
            {
                outsideTrack
            };

            NewTrackArgs newTrack = new NewTrackArgs
            {
                Tracks = tracksSingleInsideList  // Putter listen ned i tasken
            };  // opret taske


            NewTrackArgs newTrackOutside = new NewTrackArgs
            {
                Tracks = trackSingleOutsideList
            };

            uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro
            uut.OnTrackRecieved(this, newTrackOutside);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(2)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(1));
            Assert.That(results.ElementAt(1).Count, Is.EqualTo(1));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_Replace_Track_With_Same_TagID()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 10,
                Y = 10,
                Z = 10,
                depth = 1000,
                width = 2000,
                height = 500
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>(); //Antal lister modtaget med tracks

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<TracksInAirspaceArgs> tracksInAirspaceHandler = (object sender, TracksInAirspaceArgs e) =>
            {
                //int numInList = e.Tracks.Count; // Number of tracks in airspace
                //Assert.That(numInList, Is.EqualTo(0));
                var l = new List<Track>(); //Kopi af listen
                foreach (var t in e.Tracks) {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track insideTrack = new Track
            {
                X = 15,
                Y = 15,
                Altitude = 15,
                TagId = "DEF456"
            };

            List<Track> tracksSingleInsideList = new List<Track>
            {
                insideTrack
            }; // opret liste


            Track outsideTrack = new Track
            {
                X = 8,
                Y = 8,
                Altitude = 8,
                TagId = "AAA111"
            };

            List<Track> trackSingleOutsideList = new List<Track>
            {
                outsideTrack
            };

            Track outsideTrackSameTagIDasInsideTrack = new Track
            {
                X = 2,
                Y = 2,
                Altitude = 2,
                TagId = "DEF456"
            };

            List<Track> tracksOutsideListSameTagID = new List<Track>
            {
                outsideTrackSameTagIDasInsideTrack
            }; 

            NewTrackArgs newTrack = new NewTrackArgs
            {
                Tracks = tracksSingleInsideList  // Putter listen ned i tasken
            };  // opret taske


            NewTrackArgs newTrackOutside = new NewTrackArgs
            {
                Tracks = trackSingleOutsideList
            };

            NewTrackArgs newTrackOutsideSameTagID = new NewTrackArgs
            {
                Tracks = tracksOutsideListSameTagID
            };

            uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro
            uut.OnTrackRecieved(this, newTrackOutside);
            uut.OnTrackRecieved(this, newTrackOutsideSameTagID);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(3)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(1));
            Assert.That(results.ElementAt(1).Count, Is.EqualTo(1));
            Assert.That(results.ElementAt(2).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }
    }
}
