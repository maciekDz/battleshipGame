using System.Collections.Generic;
using MyBattleshipGame.Services;

namespace MyBattleshipGame.Models
{
    public interface IBoard
    {
        (int rows, int columns) BoardSize { get; }

        IEnumerable<ISquare> Squares { get; set; }
    }

    public class Board10X10 : IBoard
    {
        public IEnumerable<ISquare> Squares { get; set; }

        public (int rows, int columns) BoardSize => (10, 10);

        public Board10X10()
        {
            var squareProvider = new SquareProvider<Square>();
            Squares = squareProvider.GetSquares(BoardSize);
        }
    }
}
