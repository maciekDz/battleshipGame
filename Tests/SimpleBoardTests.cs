using MyBattleshipGame.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SimpleBoardTests
    {
        [TestCase(new[] { 1, 1 })]
        [TestCase(new[] { 4, 12 })]
        [TestCase(new[] { 5, 2 })]
        [TestCase(new[] { 10, 8 })]
        public void Board_AfterCreation_HasProperSqaresCount(int[] rowsAndColumns)
        {
            var boardSize = (rowsAndColumns[0], rowsAndColumns[1]);

            Board board = new Board(boardSize);
        }
    }
}
