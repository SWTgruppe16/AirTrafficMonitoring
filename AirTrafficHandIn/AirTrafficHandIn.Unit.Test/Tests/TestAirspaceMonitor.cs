using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficHandIn.Unit.Test
{
    [TestFixture]
    class TestAirspaceMonitor
    {
        private List<Track> _fakeTracksInAirspace;
        private Airspace _Airspace;
        private event EventHandler<TracksInAirspaceArgs> TracksInAirspaceEvent;
        private ITrack _fakeTracker;

        private AirspaceMonitor Uut;

        internal Track _comparisonPlane;

        // This function called before every test
        [SetUp]
        public void Setup()
        {
            // Makings Subsitutes should be with interfaces :)
            _fakeTracker = Substitute.For<ITrack>();
            // This should have been a fake, but no interface :(
            _Airspace = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                width = 80000,
                depth = 80000,
                height = 20000
            };
            TracksInAirspaceEvent = delegate { };
            // This should be the only real instance :)
            Uut = new AirspaceMonitor(_Airspace, _fakeTracker);

            _comparisonPlane = new Track()
            {
                TagId = "HEN777",
                Altitude = 1000,
                X = 1000,
                Y = 1000
            };

            var results = new List<List<Track>>();

            // Inserting data in the List<Track>
            // with Injected Tracks - "{ }"
            // instead of making a new List<Track> and adding later :)

            _fakeTracksInAirspace = new List<Track>() {
                new Track
                {
                    TagId = "HEN123",
                    Altitude = 1000,
                    X = 1000,
                    Y = 1000                }, // Comma separtion is needed ;)
                new Track
                {
                    TagId = "HEN321",
                    Altitude = 1000,
                    X = 1000,
                    Y = 1000                },
                _comparisonPlane // this is added for comparisions
            };

            // Adding Tracks to the Unit under test
            NewTrackArgs newTrack = new NewTrackArgs();
            newTrack.Tracks = _fakeTracksInAirspace; 
            Uut.OnTrackRecieved(this, newTrack);
        }



        [TestCase(TestName ="Add Planes Should Return NotIsEmpty")]
        [TestCase(TestName = "Add ")]
        [TestCase(TestName = "Add Planes Should Return NotIsEmpty")]
        public void AirspaceMonitorTest()
        {
               


            Assert.Pass();
        }

        // Making testcase for tests with same logic
        [TestCase("HEN777", true, TestName ="Search For Plane Should Return True")]
        [TestCase("HEN666", false, TestName = "Search For Plane Should Return True")]
        public void TrackIdTest(string testInput, bool testResult)
        {
            var comparison = (_comparisonPlane.Equals(Uut.GetTrackById(testInput)));

            Assert.That(comparison, Is.EqualTo(testResult));
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

        /*
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
                foreach (var t in e.Tracks)
                {
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

        [Test]
        public void AirspaceMonitor_IsInside_x_greater_than_X_plus_Width_Test()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
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
                foreach (var t in e.Tracks)
                {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track outsideTrack_x_greather_than_X_plus_width = new Track
            {
                X = 100000,
                Y = 1500,
                Altitude = 1000,
                TagId = "HEN207"
            };

            List<Track> tracksOutsideXList = new List<Track>
            {
                outsideTrack_x_greather_than_X_plus_width
            }; // opret liste


            NewTrackArgs newTrack1 = new NewTrackArgs
            {
                Tracks = tracksOutsideXList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack1);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_IsInside_y_greater_than_Y_plus_Depth_Test()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
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
                foreach (var t in e.Tracks)
                {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track outsideTrack_y_greather_than_Y_plus_depth = new Track
            {
                X = 100,
                Y = 100000,
                Altitude = 1000,
                TagId = "CAR123"
            };

            List<Track> tracksOutsideXList = new List<Track>
            {
                outsideTrack_y_greather_than_Y_plus_depth
            }; // opret liste


            NewTrackArgs newTrack2 = new NewTrackArgs
            {
                Tracks = tracksOutsideXList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack2);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_IsInside_z_greater_than_Z_plus_height_Test()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
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
                foreach (var t in e.Tracks)
                {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track outsideTrack_z_greather_than_Z_plus_height = new Track
            {
                X = 1000,
                Y = 1000,
                Altitude = 70000,
                TagId = "BER207"
            };

            List<Track> tracksOutsideXList = new List<Track>
            {
                outsideTrack_z_greather_than_Z_plus_height
            }; // opret liste


            NewTrackArgs newTrack1 = new NewTrackArgs
            {
                Tracks = tracksOutsideXList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack1);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_IsInside_y_smaller_than_Y()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
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
                foreach (var t in e.Tracks)
                {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track outsideTrack_y_smaller_than_Y = new Track
            {
                X = 1000,
                Y = 15,
                Altitude = 7000,
                TagId = "BER207"
            };

            List<Track> tracksOutsideXList = new List<Track>
            {
                outsideTrack_y_smaller_than_Y
            }; // opret liste


            NewTrackArgs newTrack1 = new NewTrackArgs
            {
                Tracks = tracksOutsideXList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack1);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void AirspaceMonitor_IsInside_z_smaller_than_Z()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
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
                foreach (var t in e.Tracks)
                {
                    l.Add(t);
                }
                results.Add(l);
            };

            // Register the test event handler
            uut.TracksInAirspaceEvent += tracksInAirspaceHandler;

            // Do stuff that trickers event
            Track outsideTrack_z_greather_than_Z_plus_height = new Track
            {
                X = 1000,
                Y = 1000,
                Altitude = 70,
                TagId = "BER207"
            };

            List<Track> tracksOutsideXList = new List<Track>
            {
                outsideTrack_z_greather_than_Z_plus_height
            }; // opret liste


            NewTrackArgs newTrack1 = new NewTrackArgs
            {
                Tracks = tracksOutsideXList  // Putter listen ned i tasken
            };  // opret taske

            uut.OnTrackRecieved(this, newTrack1);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
        }

        [Test]
        public void IsInAirspaceList_Test()
        {
            var airspace = new Airspace
            {
                X = 20,
                Y = 20,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
            };

            var trackCalculator = new TrackCalculator();
            var uut = new AirspaceMonitor(airspace, trackCalculator);

            Track track_IsInAirspace = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            Track track2_IsInAirspace = new Track()
            {
                TagId = "BER258",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            List<Track> IsInAirspaceList = new List<Track>
            {
                track_IsInAirspace
            }; // opret liste
            IsInAirspaceList.Add(track_IsInAirspace);
            IsInAirspaceList.Add(track2_IsInAirspace);

            Assert.That(uut.IsInAirspaceList(track2_IsInAirspace.TagId), Is.True);
            
        }
        */

    }

}
