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
        private NewTrackArgs _splitData;

        [SetUp]
        public void Setup()
        {
            ITransponderReceiver transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Splitter(transponderReceiver);
            _splitData = new NewTrackArgs();
            //_uut.newTrack +=
        }

        private void SplitDataEvent(object sender, NewTrackArgs e)
        {
            _splitData = e;
        }

    }
}
