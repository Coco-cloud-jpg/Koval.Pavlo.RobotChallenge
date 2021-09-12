using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestIsEmptyArea
    {
        [TestMethod]
        public void TestIsRequiredToCreateNewRobotWhenFourAvailableInSquare()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15) },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(16, 15) },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(17, 15) }};

            //Act
            var isVacantPlace = algorithm.isEmptyArea(robots, 0);
            //Assert
            Assert.IsTrue(isVacantPlace);
        }

        [TestMethod]
        public void TestIsRequiredToCreateNewRobotWhenFiveAvailableInSquare()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(16, 15), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(16, 16), OwnerName = "Koval Pavlo"},
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(16, 17), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(14, 17), OwnerName = "Koval Pavlo" }};

            //Act
            var isVacantPlace = algorithm.isEmptyArea(robots, 0);
            //Assert
            Assert.IsFalse(isVacantPlace);
        }
    }
}
