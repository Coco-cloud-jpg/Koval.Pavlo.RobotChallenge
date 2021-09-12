using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Koval.Pavlo.RobotChallenge.Test
{
    [TestClass]
    public class TestAlgorithm
    {
        [TestMethod]
        public void TestMoveCommand()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 3) } };
            
            //Act
            var command = algorithm.DoStep(robots, 0, map);
            //Assert
            Assert.IsTrue(command is MoveCommand);
        }

        [TestMethod]
        public void TestCollectCommand()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 200, Position = new Position(1, 1) } };

            //Act
            var command = algorithm.DoStep(robots, 0, map);
            //Assert
            Assert.IsTrue(command is CollectEnergyCommand);
        }

        public void TestCreateRobotCommandWithFreeStations()
        {
            //Arrange
            var algorithm = new KovalAlgorithm();
            var map = new Map();
            var firstStationPosition = new Position(1, 1);
            var secondStationPosition = new Position(2, 2);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = firstStationPosition, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = secondStationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>()
                                    { new Robot.Common.Robot() { Energy = 351, Position = new Position(1, 1) } };

            //Act
            var command = algorithm.DoStep(robots, 0, map);
            //Assert
            Assert.IsTrue(command is CreateNewRobotCommand);
        }
    }
}
