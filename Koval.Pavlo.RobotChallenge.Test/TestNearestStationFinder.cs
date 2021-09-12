using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestNearestStationFinder
    {
        [TestMethod]
        public void TestFindNearestFreeStations()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15) } };

            //Act
            var station = algorithm.FindNearestFreeStation(robots[0], map, robots);
            //Assert
            Assert.AreEqual(station, stationPosition);
        }

        [TestMethod]
        public void TestFindNearestFreeStationsOfSeveral()
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
            var station = algorithm.FindNearestFreeStation(robots[0], map, robots);
            //Assert
            Assert.AreEqual(station, secondStationPosition);
        }

        [TestMethod]
        public void TestFindNearestFreeStationsWithFriendOnOne()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();

            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15), OwnerName = "Koval Pavlo"},
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(1, 1), OwnerName = "Koval Pavlo" }};

            //Act
            var station = algorithm.FindNearestFreeStation(robots[0], map, robots);
            //Assert
            Assert.AreEqual(station, null);
        }

        [TestMethod]
        public void TestFindNearestFreeStationsWithEnemyOnOne()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();

            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(15, 15), OwnerName = "Koval Pavlo"},
                                      new Robot.Common.Robot() { Energy = 351, Position = new Position(1, 1), OwnerName = "Not Koval Pavlo" }};

            //Act
            var station = algorithm.FindNearestFreeStation(robots[0], map, robots);
            //Assert
            Assert.AreEqual(station, stationPosition);
        }
    }
}
