using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTrafficHandIn.Unit.Test
{
    public class TestTrackCalculator
    {
        private Track fakeA = new Track()
        {
            X = 15000,
            Y = 15000,
            Altitude = 10000,
            TimeStamp = DateTime.ParseExact("20190324081345000", "yyyyMMddHHmmssfff", null)
        };

        private Track fakeB = new Track()
        {
            X = 15000,
            Y = 16000,
            Altitude = 10000,
            TimeStamp = DateTime.ParseExact("20190324081348000", "yyyyMMddHHmmssfff", null)
        };

        TrackCalculator calculateSpeedTest = new TrackCalculator();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CalculateSpeed_TestIfSpeedCalculationIsCorrect()
        {
            calculateSpeedTest.calculateSpeed(fakeA, fakeB);
            Assert.AreEqual(333.33333333333331d, fakeB.Velocity);
        }

        private Track fakeACompassCourse = new Track()
        {
            X = 15000,
            Y = 15000
        };

        private Track fakeBCompassCourse = new Track()
        {
            X = 20000,
            Y = 20000
        };

        TrackCalculator calculateCompassCourseTest = new TrackCalculator();

        [Test]
        public void CalculateCompassCourse_TestIfCompassCourseCalculationIsCorrect()
        {
            calculateCompassCourseTest.calculateCompassCourse(fakeACompassCourse, fakeBCompassCourse);
            Assert.AreEqual(225,fakeBCompassCourse.CompassCourse);

        }
    }
}
