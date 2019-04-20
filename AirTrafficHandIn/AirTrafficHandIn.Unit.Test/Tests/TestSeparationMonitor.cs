using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTrafficHandIn.Unit.Test.Tests
{
    class TestSeparationMonitor
    {
        //private static IConditionMonitor _uut;
        private SeparationMonitor _uut;
        //private CurrentConditions _currentConditions;
        //private NewCondition _newCondition;
        //private List<SeparationMonitor> separationMonitors;

        [SetUp]
        public void Setup()
        {
            _uut = new SeparationMonitor();
        }

        private List<Track> createTestTracksList(int trackX1, int trackY1, int trackZ1, int trackX2, int trackY2,
            int trackZ2)
        {
            return new List<Track>()
            {
                new Track()
                {
                    TagId = "Car123",
                    X = trackX1,
                    Y = trackY2,
                    Altitude = trackZ1
                },
                new Track()
                {
                    TagId = "Ber123",
                    X = trackX2,
                    Y = trackY2,
                    Altitude = trackZ2
                }
            };
        }

        [TestCase(0, 0, 100, 1256, 62124, 600)]
        [TestCase(0, 0, 100, 4000, 4000, 200)]
        public void SeparationEvents_TracksNotCloseEnough_ResultIsNoSeparation(
           int trackX1, int trackY1, int trackZ1,
           int trackX2, int trackY2, int trackZ2)
        {
            var tracksNoSeparationList = createTestTracksList (trackX1, trackY1, trackZ1, trackX2, trackY2, trackZ2);
            Assert.That(_uut.ListOfConditions(tracksNoSeparationList), Is.Empty);
        }
    }
}
