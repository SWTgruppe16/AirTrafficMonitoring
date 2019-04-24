using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficHandIn.Unit.Test
{
    class TestAirspace
    {
        private Airspace _uut;
        private Track _fakeTrack;


        [SetUp]
        public void Setup()
        {
 
            _uut = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                width = 80000,
                depth = 80000,
                height = 20000 - 500
            };
        }

        [TestCase(500, 500, 501, true, TestName ="Track Should be inside")]
        [TestCase(90000, 500, 501, false, TestName = "Track Should be outside aboveX")]
        [TestCase(500, -1, 501, false, TestName = "Track Should be outside aboveY")]
        [TestCase(500, 500, 30000, false, TestName = "Track Should be outside aboveZ")]
        [TestCase(-1, 500, 501, false, TestName = "Track Should be outside underX")]
        [TestCase(500, -1, 501, false, TestName = "Track Should be outside underY")]
        [TestCase(500, 500, 0, false, TestName = "Track Should be outside underZ")]
        public void TestIsInside(int testX, int testY, int testZ, bool result)
        {
            var comparison = _uut.IsInside(testX, testY, testZ);

            Assert.That(comparison, Is.EqualTo(result));
            
        }        
    }
}
