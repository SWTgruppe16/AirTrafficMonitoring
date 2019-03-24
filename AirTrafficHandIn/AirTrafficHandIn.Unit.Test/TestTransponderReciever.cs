using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficHandIn.Unit.Test
{
    class TestTransponderReciever
    {
        private List<string> _dataRecieved;
        private ITransponderReceiver _uut; //what we want to test

        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<ITransponderReceiver>();
        }

        [Test]
        public void TransponderDataReady_RaiseEventWithTracks_RecieveTracks()
        {
            List<string> fakeData = new List<string>();
            fakeData.Add("ABC123;123456;12345;67890;20190324091230000");
            
            //_uut.TransponderDataReady += 
        }
    }
}
