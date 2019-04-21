using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficHandIn.Interfaces;
using NSubstitute;
using NUnit.Framework;





namespace AirTrafficHandIn.Unit.Test
{
    [TestFixture]
    class TestSeperation
    {
        private IConditionMonitor uut;
        private AirspaceMonitor fakeAirspaceMonitor_;

        [SetUp]
        public void Setup()
        {
            this.uut = new SeparationMonitor();
        }

        [Test]
        public void IsToClose_TracksToClose()
        {
            var _uut = new SeparationMonitor();

            var a = new Track
            {
                TagId = "HEN123",
                Altitude = 10,
                X = 10,
                Y = 10
            };

            var b = new Track
            {
                TagId = "HEN321",
                Altitude = 10,
                X = 10,
                Y = 10
            };

            Assert.That(_uut.IsToClose(a, b), Is.True);
        }

        [Test]
        public void IsToClose_TracksNotToClose()
        {
            var _uut = new SeparationMonitor();

            var a = new Track
            {
                TagId = "HEN123",
                Altitude = 10000,
                X = 10000,
                Y = 10000
            };

            var b = new Track
            {
                TagId = "HEN321",
                Altitude = 10,
                X = 10,
                Y = 10
            };

            Assert.That(_uut.IsToClose(a, b), Is.False);
        }

        [Test]
        public void NoSeparationConditionOccuredTest()
        {


            // To keep result(s) after the event(s) has fired
            var results = new List<List<ICondition>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<NewConditionArgs> tracksInAirspaceHandler = (object sender, NewConditionArgs e) =>
            {
                results.Add(e.Conditions);
            };


            // Do stuff that trickers event
            List<Track> tracksEmptyList = new List<Track>(); // opret liste
                                                             // Evt tilføj ting på listen
            var newTrack = new TracksInAirspaceArgs
            {
                Tracks = tracksEmptyList  // Putter listen ned i tasken
            };  // opret taske

            
            // Register the test event handler
            this.uut.NewConditionsEvent += tracksInAirspaceHandler;

            this.uut.OnTrackRecieved(this, newTrack); // Giv tasken til Caro

            // deregister the event because we are good boys and girls
            this.uut.NewConditionsEvent -= tracksInAirspaceHandler;


            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(0)); // Zero events should be fired if no condition is met

            
        }

        [Test]
        public void SeparationConditionOccuredTest()
        {
            // To keep result(s) after the event(s) has fired
            var results = new List<List<ICondition>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<NewConditionArgs> tracksInAirspaceHandler = (object sender, NewConditionArgs e) =>
            {
                results.Add(e.Conditions);
            };

            

            // Register the test event handler

            // Do stuff that trickers event
            var tracks = new List<Track>(); // opret liste


            //opretter to fly, med condition imellem

            var a1 = new Track
            {
                TagId = "HEN123",
                Altitude = 10,
                X = 10,
                Y = 10
            };

            var b1 = new Track
            {
                TagId = "HEN321",
                Altitude = 10,
                X = 10,
                Y = 10
            };

            tracks.Add(a1);          // tilføjer to fly til min liste
            tracks.Add(b1);
            

            var tracksInAirspaceArgs = new TracksInAirspaceArgs
            {
                Tracks = tracks  // Putter listen ned i tasken
            };  // opret taske

            uut.NewConditionsEvent += tracksInAirspaceHandler;

            uut.OnTrackRecieved(this, tracksInAirspaceArgs); // Giv tasken til Caro

            // deregister the event because we are good boys and girls
            uut.NewConditionsEvent -= tracksInAirspaceHandler;


            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // 1 events should be fired if 1 condition is met

        }


        [Test]
        public void OneSeparationConditionOccuredWithFourFligthsInAirspaceTest()
        {
            // To keep result(s) after the event(s) has fired
            var results = new List<List<ICondition>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            EventHandler<NewConditionArgs> tracksInAirspaceHandler = (object sender, NewConditionArgs e) =>
            {
                results.Add(e.Conditions);
            };



            // Register the test event handler

            // Do stuff that trickers event
            var tracks = new List<Track>
            {
                new Track
                {
                    TagId = "HEN123",
                    Altitude = 10,
                    X = 10,
                    Y = 10
                },
                new Track
                {
                    TagId = "HEN321",
                    Altitude = 10,
                    X = 10,
                    Y = 10
                },
                new Track
                {
                    TagId = "Car123",
                    Altitude = 10,
                    X = 10,
                    Y = 10
                },
                new Track
                {
                    TagId = "Car321",
                    Altitude = 1000,
                    X = 1000,
                    Y = 1000
                }

            };// opret liste


            //opretter 4 fly, med kun 1 condition imellem

          

            var tracksInAirspaceArgs = new TracksInAirspaceArgs
            {
                Tracks = tracks  // Putter listen ned i tasken
            };  // opret taske

            uut.NewConditionsEvent += tracksInAirspaceHandler;

            uut.OnTrackRecieved(this, tracksInAirspaceArgs); // Giv tasken til Caro

            // deregister the event because we are good boys and girls
            uut.NewConditionsEvent -= tracksInAirspaceHandler;


            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // 1 events should be fired if 1 condition is met

        }

    }
    
}
    //    public delegate void AnswerHandler(object sender, AnswerEventArgs e);

        //    public class AnswerEventArgs : EventArgs
        //    {
        //        public int Answer { get; set; }
        //    }

        //    public class AnswerFinder
        //    {
        //        public event AnswerHandler AnswerEvent;

        //        public void FindTheAnswer(int? value)
        //        {
        //            int? answer = value;
        //            if (answer.HasValue)
        //            {
        //                var eventArgs = new AnswerEventArgs { Answer = answer.Value };
        //                AnswerEvent(this, eventArgs);
        //            }
        //        }
        //    }

        //    [TestFixture]
        //    class TestSeperation
        //    {
        //        //private static ICondition _uut;

        //        [SetUp]
        //        public void Setup()
        //        {
        //            //_uut = new SeperationCondetion("");
        //        }

        //        [Test]
        //        public void SeperationTest()
        //        {
        //            // Out uut
        //            var uut = new SeperationMonitor();

        //            // To keep result(s) after the event(s) has fired
        //            List<int> results = new List<int>();

        //            // Our test handler
        //            // This validates that the events arguments are correct
        //            // Here we verify that the event indeed had 42 as expected
        //            // And we save the value to results, such that we can verify how many times 
        //            // the event fired and they all were correct
        //            AnswerHandler handler = (sender, e) =>
        //            {
        //                Assert.That(e.Answer, Is.EqualTo(42));
        //                results.Add(e.Answer);
        //            };

        //            // Register the test event handler
        //            uut.AnswerEvent += handler;

        //            // Do stuff that trickers event
        //            uut.FindTheAnswer(42);   // Fx could be the OnTrackRecieved method in AirSpaceMonitor

        //            // Verify the amount of events
        //            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

        //            // and their value(s)
        //            Assert.That(results.ElementAt(0), Is.EqualTo(42));

        //            // deregister the event because we are good boys and girls
        //            uut.AnswerEvent -= handler;
        //        }

        //        [Test]
        //        public void Attempt2()
        //        {
        //            // Out uut
        //            var uut = new AnswerFinder();

        //            // To keep result(s) after the event(s) has fired
        //            List<int> results = new List<int>();

        //            // Our test handler
        //            // This validates that the events arguments are correct
        //            // Here we verify that the event indeed had 42 as expected
        //            // And we save the value to results, such that we can verify how many times 
        //            // the event fired and they all were correct
        //            AnswerHandler handler = (sender, e) =>
        //            {
        //                Assert.That(e.Answer, Is.EqualTo(42));
        //                results.Add(e.Answer);
        //            };

        //            // Register the test event handler
        //            uut.AnswerEvent += handler;

        //            // Do stuff that trickers event
        //            uut.FindTheAnswer(42);

        //            // Verify the amount of events
        //            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

        //            // and their value(s)
        //            Assert.That(results.ElementAt(0), Is.EqualTo(42));

        //            // deregister the event because we are good boys and girls
        //            uut.AnswerEvent -= handler;
        //        }
