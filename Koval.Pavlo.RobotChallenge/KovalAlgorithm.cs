using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koval.Pavlo.RobotChallenge
{
    public class KovalAlgorithm : IRobotAlgorithm
    {
        public string Author => "Koval Pavlo";
        const int energyForNewRobot = 100;
        const int energyForPushing = 10;
        public int roundCount { get; set; }
        private bool isEnemyOne = false;
        public KovalAlgorithm()
        {
            Logger.OnLogRound += new LogRoundEventHandler(Logger_OnLogRound);
        }

        private void Logger_OnLogRound(object sender, LogRoundEventArgs e) => ++roundCount;

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];
            var myRobotsCount = robots.Where(item => item.OwnerName == this.Author).Count();

            if ((movingRobot.Energy > 350) &&
                isEmptyArea(robots as List<Robot.Common.Robot>, robotToMoveIndex) &&
                (myRobotsCount < FreeStationCount(map, robots as List<Robot.Common.Robot>)) 
                && roundCount <= 49
                && myRobotsCount < 100
                && WillTheChildBeInAbundance(map, robots as List<Robot.Common.Robot>, movingRobot))
            {
                return new CreateNewRobotCommand();
            }

            Position stationPosition = FindNearestFreeStation(movingRobot, map, robots);

            if (stationPosition == null)
            {
                return null;
            }

            if (stationPosition == movingRobot.Position)
                return new CollectEnergyCommand();
            else
            {
                Position newPosition = stationPosition;
                int distance = DistanceHelper.FindDistance(newPosition, movingRobot.Position);

                if (distance + (isEnemyOne? energyForPushing : 0) >= movingRobot.Energy)
                {
                    int dx = 0, dy = 0;

                    dx = Math.Sign(stationPosition.X - movingRobot.Position.X);
                    dy = Math.Sign(stationPosition.Y - movingRobot.Position.Y);

                    newPosition = new Position(movingRobot.Position.X + dx, movingRobot.Position.Y + dy);
                }

                return new MoveCommand() { NewPosition = newPosition };
            }
        }

        public bool WillTheChildBeInAbundance(Map map, List<Robot.Common.Robot> robots, Robot.Common.Robot movingGuy)
        {
            Position closest = FindNearestFreeStation(movingGuy, map, robots);
            int offset = 10;

            return DistanceHelper.FindDistance(movingGuy.Position, closest) + (isEnemyOne ? energyForPushing : 0) + offset < energyForNewRobot;
        }
        public bool isEmptyArea(List<Robot.Common.Robot> robots, int moveRobotIndex)
        {
            var moveRobot = robots[moveRobotIndex];

            var countRobotsNear = robots.Where(item => item.Position.X >= moveRobot.Position.X - 2 &&
                                                       item.Position.X <= moveRobot.Position.X + 2 &&
                                                       item.Position.Y >= moveRobot.Position.Y - 2 &&
                                                       item.Position.Y <= moveRobot.Position.Y + 2 &&
                                                       item.OwnerName == this.Author).Count();

            return countRobotsNear > 3 ? false : true;
        }

        public int FreeStationCount(Map map, List<Robot.Common.Robot> robots)
        {
            var stationPositions = map.Stations.Select(item => item.Position).ToList();
            var robotsPositions = robots.Where(item => item.OwnerName == this.Author).Select(item => item.Position).ToList();

            return stationPositions.Where(item => !robotsPositions.Contains(item)).Count();
        }

        /*public Position GetNearestPosition(List<Position> positions, Position robotPosition)
        {
            Position nearestPosition = null;
            int minLength = DistanceHelper.FindDistance(positions[0], robotPosition);

            foreach (var pos in positions)
            {
                if (DistanceHelper.FindDistance(pos, robotPosition) < minLength)
                {
                    minLength = DistanceHelper.FindDistance(pos, robotPosition);
                    nearestPosition = pos;
                }
            }

            return nearestPosition;
        }

        public List<Position> FindEmptyPositionNearStation(Position stationPosition, List<Robot.Common.Robot> robots)
        {
            List<Position> allPositions = new List<Position>();

            if (stationPosition.X > 0 && stationPosition.X < 99 && stationPosition.Y > 0 && stationPosition.Y < 99)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (i == 0 && y == 0)
                        {
                            continue;
                        }

                        allPositions.Add(new Position(stationPosition.X + i, stationPosition.Y + y));
                    }
                }
            }
            else
            {
                return null;
            }

            var robotsPosition = robots.Where(item => allPositions.Contains(item.Position) && item.OwnerName == this.Author).Select(item => item.Position).ToList();
            allPositions = allPositions.Where(item => !robotsPosition.Contains(item)).ToList();

            return allPositions;
        }
        public int CalculateRobotNearStation(Position stationPosition, List<Robot.Common.Robot> robots)
        {
            return robots.Where(robot =>
            {
                return robot.Position.X >= stationPosition.X - 1 && robot.Position.X <= stationPosition.X + 1 &&
                    robot.Position.Y >= stationPosition.Y - 1 && robot.Position.Y <= stationPosition.Y + 1 &&
                    robot.OwnerName == this.Author;
            }).Count();
        }*/

        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;

            foreach (var station in map.Stations)
            {
                if (IsStationFree(station, movingRobot, robots))
                {
                    int d = DistanceHelper.FindDistance(station.Position, movingRobot.Position);

                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }

            return nearest == null ? null : nearest.Position;
        }
        public bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            return IsCellFree(station.Position, movingRobot, robots);
        }
        public bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot.OwnerName != this.Author && robot.Position == cell)
                {
                    isEnemyOne = true;

                    return true;
                }

                if (robot != movingRobot && robot.OwnerName == this.Author && robot.Position == cell)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
