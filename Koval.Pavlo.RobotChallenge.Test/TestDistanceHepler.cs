using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using Koval.Pavlo.RobotChallenge;
using System;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestDistanceHepler
    {
        [TestMethod]
        public void TestDisance()
        {
            //Arrange
            var p1 = new Position(5, 2);
            var p2= new Position(1, 4);

            //Act
            var result = DistanceHelper.FindDistance(p1, p2);

            //Assert
            Assert.AreEqual(20, result);
        }
    }
}
