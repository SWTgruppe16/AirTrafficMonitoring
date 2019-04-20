using AirTrafficHandIn.Unit.Test.Tests;
using NSubstitute;
using NUnit.Framework;
using System;

namespace AirTrafficHandIn.Unit.Test.Tests
{
    [TestFixture]
    public class TestSeparationMonitorTests
    {


        [SetUp]
        public void SetUp()
        {

        }


        private TestSeparationMonitor CreateTestSeparationMonitor()
        {
            return new TestSeparationMonitor();
        }

        [Test]
        public void Setup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateTestSeparationMonitor();

            // Act
            unitUnderTest.Setup();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SeparationEvents_TracksNotCloseEnough_ResultIsNoSeparation_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateTestSeparationMonitor();
            int trackX1 = 0;
            int trackY1 = 0;
            int trackZ1 = 100;
            int trackX2 = 4000;
            int trackY2 = 4000;
            int trackZ2 = 200;

            // Act
            unitUnderTest.SeparationEvents_TracksNotCloseEnough_ResultIsNoSeparation(
                trackX1,
                trackY1,
                trackZ1,
                trackX2,
                trackY2,
                trackZ2);

            // Assert
            Assert.Fail();
        }
    }
}
