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
        public void SplitData_CheckIf_SplitData_IsCorrect_True_Test()
        {
            var trackData = new List<string>
            {
                "CAR054;27450;19500;2500;20190411123156789"
            };

            var RawTestData = new RawTransponderDataEventArgs(trackData);

            Track TrackData1 = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            Track correctTrackData = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            tracks.Add(TrackData1);

            _uut.OnTransponderData(null, RawTestData);

            foreach (var track in tracks)
            {
                Assert.That(track.TagId, Is.EqualTo(correctTrackData.TagId));
                Assert.That(track.X, Is.EqualTo(correctTrackData.X));
                Assert.That(track.Y, Is.EqualTo(correctTrackData.Y));
                Assert.That(track.Altitude, Is.EqualTo(correctTrackData.Altitude));
                Assert.That(DateTime.Compare(track.TimeStamp, correctTrackData.TimeStamp), Is.Zero);
            }
        }

        [Test]
        public void SplitData_CheckIf_SplitData_IsCorrect_False_Test2()
        {
            var trackData = new List<string>
            {
                "CAR054;27450;19500;2500;20190411123156789"
            };

            var RawTestData = new RawTransponderDataEventArgs(trackData);

            Track TrackData1 = new Track()
            {
                TagId = "BER123",
                X = 75000,
                Y = 24556,
                Altitude = 740,
                TimeStamp = DateTime.ParseExact("20160411123156789", "yyyyMMddHHmmssfff", null)
            };

            Track TrackData2 = new Track()
            {
                TagId = "BER456",
                X = 77000,
                Y = 22556,
                Altitude = 790,
                TimeStamp = DateTime.ParseExact("20180411123156789", "yyyyMMddHHmmssfff", null)
            };

            Track TrackData3 = new Track()
            {
                TagId = "BER789",
                X = 70000,
                Y = 27556,
                Altitude = 720,
                TimeStamp = DateTime.ParseExact("20170411123156789", "yyyyMMddHHmmssfff", null)
            };

            Track correctTrackData = new Track()
            {
                TagId = "BER257",
                X = 74000,
                Y = 23556,
                Altitude = 750,
                TimeStamp = DateTime.ParseExact("20190411123156789", "yyyyMMddHHmmssfff", null)
            };

            tracks.Add(TrackData1);
            tracks.Add(TrackData2);
            tracks.Add(TrackData3);

            _uut.OnTransponderData(null, RawTestData);

            foreach (var track in tracks)
            {
                Assert.That(track.TagId, Is.Not.EqualTo(correctTrackData.TagId));
                Assert.That(track.X, Is.Not.EqualTo(correctTrackData.X));
                Assert.That(track.Y, Is.Not.EqualTo(correctTrackData.Y));
                Assert.That(track.Altitude, Is.Not.EqualTo(correctTrackData.Altitude));
                Assert.That(DateTime.Compare(track.TimeStamp, correctTrackData.TimeStamp), Is.Not.Zero);
            }
        }
    }
}
