using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficHandIn.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficHandIn.Unit.Test
{
    class TestSplitter
    {
        private ISplitter _uut;
        private List<Track> tracks;

        [SetUp]
        public void Setup()
        {
            ITransponderReceiver transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Splitter();
            transponderReceiver.TransponderDataReady += _uut.OnTransponderData;
            tracks = new List<Track>();
        }


        [Test]
        public void CheckIf_Splitter_OnlySplits_One_String_test()
        {
            // Create track data
            var trackData = new List<string>
            {
                "HEN207;23550;24500;7500;20190411123156789"
            };

            // Pak data i wrapper
            var RawTestData = new RawTransponderDataEventArgs(trackData);

            // Fake event
            _uut.OnTransponderData(null, RawTestData);

            Assert.That(trackData, Has.Count.EqualTo(1)); //Test that the splitter only split one string array
        }

        [Test]
        public void SplitData_CheckIf_SplitData_IsCorrect_Test()
        {
            var trackData = new List<string>
            {
                "CAR054;27450;19500;2500;20190411123156789"
            };

            var RawTestData = new RawTransponderDataEventArgs(trackData);

            Track correcTrackData = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            _uut.OnTransponderData(null, RawTestData);

            foreach (var track in tracks)
            {
                Assert.That(track.TagId, Is.EqualTo(correcTrackData.TagId));
                Assert.That(track.X, Is.EqualTo(correcTrackData.X));
                Assert.That(track.Y, Is.EqualTo(correcTrackData.Y));
                Assert.That(track.Altitude, Is.EqualTo(correcTrackData.Altitude));
                Assert.That(DateTime.Compare(track.TimeStamp, correcTrackData.TimeStamp), Is.Zero);
            }
        }
    }
}
