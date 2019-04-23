using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AirTrafficHandIn.Unit.Test
{
    [TestFixture]
    class TestTrack
    {
        private Track uut;

        [SetUp]
        public void SetUp()
        {
            uut = new Track();
        }

        [Test]
        public void ToString_Test()
        {
            var uut = new Track();

            Track track = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            string result = track.ToString();
            string expectedString = "{ID: " + track.TagId + " (" + track.X + "," + track.Y + "," + track.Altitude + ") Speed: " +
                                 track.Velocity + "}";
            Assert.AreEqual(expectedString, result);
        }

    }
}
