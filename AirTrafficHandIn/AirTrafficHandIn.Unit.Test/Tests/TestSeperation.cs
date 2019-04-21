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
        private SeparationMonitor uut;
        private AirspaceMonitor fakeAirspaceMonitor_;

        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void NoSeparationConditionOccuredTest()
        {
            // Out uut
            var airspace = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                depth = 80000,
                width = 80000,
                height = 20000
            };

            var separationCondition = new SeparationCondition(DateTime.Now, "1", "2");
            var uut = new SeparationMonitor();

            // To keep result(s) after the event(s) has fired
            List<List<Track>> results = new List<List<Track>>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had as expected
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

            uut.OnTrackRecieved (this, newTrack); // Giv tasken til Caro

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0).Count, Is.EqualTo(0));

            // deregister the event because we are good boys and girls
            uut.TracksInAirspaceEvent -= tracksInAirspaceHandler;
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
