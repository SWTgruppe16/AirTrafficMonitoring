using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace AirTrafficHandIn.Unit.Test
{
    class TestAirspace
    {
        private Airspace uut;

        [SetUp]
        public void Setup()
        {
            //var _uut = new Airspace
            //{
            //    X = 0,
            //    Y = 0,
            //    Z = 500,
            //    width = 80000,
            //    depth = 80000,
            //    height = 20000 - 500
            //};
        }

        [Test]
        public void CheckIf_airspace_is_equal_to_airspaceCompare()
        {
            var airspace = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                width = 80000,
                depth = 80000,
                height = 20000 - 500
            };

            var airspaceCompare = new Airspace
            {
                X = 0,
                Y = 0,
                Z = 500,
                width = 80000,
                depth = 80000,
                height = 20000 - 500
            };

            Assert.That(airspace == airspaceCompare, Is.True);


        }
    }
}
