using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services;
using MyBattleshipGame.Tests.TestModels;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class BasicSquareProviderTests
    {
        [TestCaseSource(nameof(BoardSize))]
        public void Squares_AfterCreation_HaveProperSquaresCount((int rows, int columns) boardSize)
        {
            var squaresCount = boardSize.rows * boardSize.columns;

            var squareProvider = new SquareProvider<TestSquare>();
            var squares = squareProvider.GetSquares(boardSize);

            Assert.AreEqual(squaresCount, squares.Count());
        }

        [TestCaseSource(nameof(BoardSize))]
        public void Board_AfterCreation_HasProperlyAssignedNeighbours((int rows, int columns) boardSize)
        {
            var squareProvider = new SquareProvider<TestSquare>();
            var squares = squareProvider.GetSquares(boardSize);

            foreach (var square in squares)
            {
                var allNeighbours = square.AllNeighbours.ToList();
                var allNeighboursUniqueAddresses = square.AllNeighbours.Select(x => x.Address).Distinct().ToList();

                Assert.AreEqual(allNeighboursUniqueAddresses.Count(), allNeighbours.Count());
                Assert.That(!allNeighboursUniqueAddresses.Contains(square.Address));
            }
        }

        private static IEnumerable<TestCaseData> BoardSize
        {
            get
            {
                yield return new TestCaseData((10, 10));
                yield return new TestCaseData((8, 10));
                yield return new TestCaseData((15, 5));
                yield return new TestCaseData((23, 54));
                yield return new TestCaseData((100, 7));
                yield return new TestCaseData((5, 11));
                yield return new TestCaseData((1, 23));
                yield return new TestCaseData((71, 8));
                yield return new TestCaseData((66, 9));
                yield return new TestCaseData((2, 2));
            }
        }
    }
}
