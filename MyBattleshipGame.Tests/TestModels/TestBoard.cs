using System.Collections.Generic;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services;

namespace MyBattleshipGame.Tests.TestModels
{
    public class TestBoard : IBoard
    {
        public IEnumerable<ISquare> Squares { get; set; }

        public (int rows, int columns) BoardSize { get; }

        public TestBoard()
        {
            BoardSize = (10, 10);
            Squares = new TestSquareProvider<TestSquare>().GetSquares(BoardSize);
        }

        public TestBoard((int rows, int columns) boardSize)
        {
            BoardSize = boardSize;
            Squares = new TestSquareProvider<TestSquare>().GetSquares((10, 10));
        }
    }
}
