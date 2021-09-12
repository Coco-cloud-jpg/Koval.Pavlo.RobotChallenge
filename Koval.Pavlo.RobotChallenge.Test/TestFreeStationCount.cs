using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestFreeStationCount
    {
        [TestMethod]
        public void TestFreeStationCountWithoutRobotsOnThem()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(1, 1);
            var secondStationPosition = new Position(6, 6);
            var thirdStationPosition = new Position(80, 80);
            map.Stations = new List<EnergyStation>{
                new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = secondStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = thirdStationPosition, RecoveryRate = 2 },
            };

            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15) } };

            //Act
            var count = algorithm.FreeStationCount(map, robots);
            //Assert
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void TestFreeStationCountWithFriendRobotsOnThem()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(1, 1);
            var secondStationPosition = new Position(6, 6);
            var thirdStationPosition = new Position(80, 80);
            map.Stations = new List<EnergyStation>{
                new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = secondStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = thirdStationPosition, RecoveryRate = 2 },
            };

            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(1, 1), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(80, 80),OwnerName = "Koval Pavlo"  }};

            //Act
            var count = algorithm.FreeStationCount(map, robots);
            //Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestFreeStationCountWithEnemiesRobotsOnThem()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(1, 1);
            var secondStationPosition = new Position(6, 6);
            var thirdStationPosition = new Position(80, 80);
            map.Stations = new List<EnergyStation>{
                new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = secondStationPosition, RecoveryRate = 2 },
                new EnergyStation() { Energy = 1000, Position = thirdStationPosition, RecoveryRate = 2 },
            };

            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15), OwnerName = "Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(1, 1), OwnerName = "Not Koval Pavlo" },
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(80, 80),OwnerName = "Not Koval Pavlo"  }};

            //Act
            var count = algorithm.FreeStationCount(map, robots);
            //Assert
            Assert.AreEqual(3, count);
        }
    }
}
