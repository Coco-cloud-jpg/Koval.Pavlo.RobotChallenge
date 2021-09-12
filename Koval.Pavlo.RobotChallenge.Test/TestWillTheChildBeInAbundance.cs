using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestWillTheChildBeInAbundance
    {
        [TestMethod]
        public void TestItIsNeededToCreateNewRobotInAreaWithVacantStations()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(1, 1);
            var secondStationPosition = new Position(6, 6);
            map.Stations = new List<EnergyStation>{
                new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = secondStationPosition, RecoveryRate = 2 },
            };

            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(5, 15) } };

            //Act
            var isNeedOfNewRobot = algorithm.WillTheChildBeInAbundance(map, robots, robots[0]);
            //Assert
            Assert.IsTrue(isNeedOfNewRobot);
        }

        [TestMethod]
        public void TestItIsNeededToCreateNewRobotInAreaWithoutVacantStations()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(6, 6);
            map.Stations = new List<EnergyStation>{
                new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 },
            };

            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(8, 16), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(6, 6), OwnerName = "Not Koval Pavlo" }};

            //Act
            var isNeedOfNewRobot = algorithm.WillTheChildBeInAbundance(map, robots, robots[0]);
            //Assert
            Assert.IsFalse(isNeedOfNewRobot);
        }
    }
}
