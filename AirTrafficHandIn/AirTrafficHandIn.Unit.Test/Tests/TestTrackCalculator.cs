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
        private TrackCalculator _uut;
        private Track fakeA;
        private Track fakeB;
        private Track fakeACompassCourse;
        private Track fakeBCompassCourse;



        //TrackCalculator calculateSpeedTest = new TrackCalculator();

        [SetUp]
        public void Setup()
        {
            fakeA = new Track
            {
                X = 15000,
                Y = 15000,
                Altitude = 10000,
                TimeStamp = DateTime.ParseExact("20190324081345000", "yyyyMMddHHmmssfff", null)
            };

            fakeB = new Track
            {
                X = 15000,
                Y = 16000,
                Altitude = 10000,
                TimeStamp = DateTime.ParseExact("20190324081348000", "yyyyMMddHHmmssfff", null)
            };

            fakeBCompassCourse = new Track
            {
                X = 20000,
                Y = 20000
            };

            fakeACompassCourse = new Track()
            {
                X = 15000,
                Y = 15000
            };

        }

        [Test]
        public void CalculateSpeed_Test_If_Speed_Calculation_Is_Correct()
        {
        var uut_speedtest = new TrackCalculator();
    
        uut_speedtest.calculateSpeed(fakeA, fakeB);
        Assert.AreEqual(333.33333333333331d, fakeB.Velocity);
        }

        [Test]
        public void CalculateSpeed_Test_If_Speed_Calculation_Is_Not_Correct()
        {
            TrackCalculator calculateSpeedTest = new TrackCalculator();
            calculateSpeedTest.calculateSpeed(fakeA, fakeB);
            Assert.AreNotEqual(133.33333333333331d, fakeB.Velocity);
        }

        [Test]
        public void CalculateCompassCourse_Test_If_Compass_Course_Calculation_Is_Correct()
        {
        TrackCalculator calculateCompassCourseTest = new TrackCalculator();
        calculateCompassCourseTest.calculateCompassCourse(fakeACompassCourse, fakeBCompassCourse);
        Assert.AreEqual(225, fakeBCompassCourse.CompassCourse);

        }

        [Test]
        public void CalculateCompassCourse_Test_If_Compass_Course_Calculation_Is_not_Correct()
        {
            TrackCalculator calculateCompassCourseTest = new TrackCalculator();
            calculateCompassCourseTest.calculateCompassCourse(fakeACompassCourse, fakeBCompassCourse);
            Assert.AreNotEqual(255, fakeBCompassCourse.CompassCourse);

        }

    }

}
