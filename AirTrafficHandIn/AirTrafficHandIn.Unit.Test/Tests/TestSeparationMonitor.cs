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
        private SeparationMonitor _uut;
        private CurrentConditions _currentConditions;
        private NewCondition _newCondition;
        private List<SeparationMonitor> separationMonitors;

        [SetUp]
        public void Setup()
        {

            _uut = new SeparationMonitor();

        }
        
    }
}
