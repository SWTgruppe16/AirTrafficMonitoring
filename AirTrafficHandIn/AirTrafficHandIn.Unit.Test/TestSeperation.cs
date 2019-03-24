using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;





namespace AirTrafficHandIn.Unit.Test
{
    public delegate void AnswerHandler(object sender, AnswerEventArgs e);
    
    public class AnswerEventArgs : EventArgs
    {
        public int Answer { get; set; }
    }

    public class AnswerFinder
    {
        public event AnswerHandler AnswerEvent;

        public void FindTheAnswer(int? value)
        {
            int? answer = value;
            if (answer.HasValue)
            {
                var eventArgs = new AnswerEventArgs { Answer = answer.Value };
                AnswerEvent(this, eventArgs);
            }
        }
    }

    [TestFixture]
    class TestSeperation
    {
        //private static ICondition _uut;

        [SetUp]
        public void Setup()
        {
            //_uut = new SeperationCondetion("");
        }

        [Test]
        public void Attempt1()
        {
            // Out uut
            var uut = new AnswerFinder();

            // To keep result(s) after the event(s) has fired
            List<int> results = new List<int>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            AnswerHandler handler = (sender, e) =>
            {
                Assert.That(e.Answer, Is.EqualTo(42));
                results.Add(e.Answer);
            };

            // Register the test event handler
            uut.AnswerEvent += handler;

            // Do stuff that trickers event
            uut.FindTheAnswer(42);   // Fx could be the OnTrackRecieved method in AirSpaceMonitor

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0), Is.EqualTo(42));

            // deregister the event because we are good boys and girls
            uut.AnswerEvent -= handler;
        }

        [Test]
        public void Attempt2()
        {
            // Out uut
            var uut = new AnswerFinder();

            // To keep result(s) after the event(s) has fired
            List<int> results = new List<int>();

            // Our test handler
            // This validates that the events arguments are correct
            // Here we verify that the event indeed had 42 as expected
            // And we save the value to results, such that we can verify how many times 
            // the event fired and they all were correct
            AnswerHandler handler = (sender, e) =>
            {
                Assert.That(e.Answer, Is.EqualTo(42));
                results.Add(e.Answer);
            };

            // Register the test event handler
            uut.AnswerEvent += handler;

            // Do stuff that trickers event
            uut.FindTheAnswer(42);

            // Verify the amount of events
            Assert.That(results.Count, Is.EqualTo(1)); // Only one event must be fired in this test

            // and their value(s)
            Assert.That(results.ElementAt(0), Is.EqualTo(42));

            // deregister the event because we are good boys and girls
            uut.AnswerEvent -= handler;
        }
    }
}
