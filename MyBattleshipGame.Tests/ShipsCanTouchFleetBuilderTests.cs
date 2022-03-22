using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services;
using MyBattleshipGame.Tests.TestModels;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class ShipsCanTouchFleetBuilderTests
    {
        [TestCaseSource(nameof(BoardSizeAndFleetDetails))]
        public void FleetBuilder_ReturnsValidShipCount_On10X10Territory((int rows, int columns) boardSize, (int shipSize, int numOfShips, string shipName)[] fleetDetails)
        {
            //Arrange
            var board = new TestBoard(boardSize);
            var fleetBuilder = new ShipsCanTouchFleetBuilder<TestShip>(fleetDetails);

            //Act
            var fleet = fleetBuilder.BuildFleet(board.Squares);

            //Assert
            Assert.AreEqual(fleet.Count(), fleetDetails.Sum(x => x.numOfShips));
        }

        [TestCaseSource(nameof(BoardSizeAndFleetDetails))]
        public void FleetBuilder_ReturnsValidMastCount_On10X10Territory((int rows, int columns) boardSize, (int shipSize, int numOfShips, string shipName)[] fleetDetails)
        {
            //Arrange
            var board = new TestBoard(boardSize);
            var fleetBuilder = new ShipsCanTouchFleetBuilder<TestShip>(fleetDetails);
            var mastCount = 0;
            foreach (var (shipSize, numOfShips, shipName) in fleetDetails)
            {
                mastCount += shipSize * numOfShips;
            }

            //Act
            var fleet = fleetBuilder.BuildFleet(board.Squares);

            //Assert
            Assert.AreEqual(fleet.SelectMany(x => x.Masts).Count(), mastCount);
        }

        [TestCaseSource(nameof(BoardSizeAndFleetDetails))]
        public void FleetBuilder_ReturnsNonOverlappingShips_On10X10Territory((int rows, int columns) boardSize, (int shipSize, int numOfShips, string shipName)[] fleetDetails)
        {
            //Arrange
            var board = new TestBoard(boardSize);
            var fleetBuilder = new ShipsCanTouchFleetBuilder<TestShip>(fleetDetails);

            //Act
            var fleet = fleetBuilder.BuildFleet(board.Squares).ToList();

            //Assert
            foreach (var ship in fleet)
            {
                var masts = ship.Masts;
                var otherMasts = fleet.Where(x => x != ship).SelectMany(x => x.Masts);

                Assert.That(!masts.Any(x => otherMasts.Contains(x)));
            }
        }

        private static IEnumerable<TestCaseData> BoardSizeAndFleetDetails
        {
            get
            {
                yield return new TestCaseData((10, 10), new[] { (5, 2, ""), (4, 2, ""), (3, 2, ""), (3, 2, ""), (2, 2, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 1, ""), (4, 1, ""), (3, 1, ""), (3, 1, ""), (2, 1, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 2, ""), (4, 2, ""), (3, 2, ""), (3, 2, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 1, ""), (4, 1, ""), (3, 1, ""), (3, 1, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 2, ""), (4, 2, ""), (3, 2, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 1, ""), (4, 1, ""), (3, 1, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 2, ""), (4, 2, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 1, ""), (4, 1, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 2, "") });
                yield return new TestCaseData((10, 10), new[] { (5, 1, "") });
            }
        }
    }
}
