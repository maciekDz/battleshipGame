using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class SquareProvider<T> : ISquareProvider<T> where T : ISquare, new()
    {
        public IEnumerable<T> GetSquares((int rows, int columns) boardSize)
        {
            var squares = new List<T>();

            PopulateSquares(boardSize, squares);
            AssociatePerpendicularNeighbours(squares);
            AssociateDiagonalNeighbours(squares);

            return squares;
        }

        private void PopulateSquares((int rows, int columns) boardSize, IList<T> squares)
        {
            for (var i = 1; i <= boardSize.rows; i++)
            {
                for (var j = 1; j <= boardSize.columns; j++)
                {
                    squares.Add(new T()
                    {
                        Row = i,
                        Column = j
                    });
                }
            }
        }

        private void AssociatePerpendicularNeighbours(IList<T> squares)
        {
            foreach (var square in squares)
            {
                var neighbours = squares.Where(x =>
                    (x.Row == square.Row && (x.Column == square.Column + 1 || x.Column == square.Column - 1)) ||
                    (x.Column == square.Column && (x.Row == square.Row + 1 || x.Row == square.Row - 1)));

                square.PerpendicularNeighbours = (IEnumerable<ISquare>)neighbours;
            }
        }

        private void AssociateDiagonalNeighbours(IList<T> squares)
        {
            foreach (var square in squares)
            {
                var neighbours = squares.Where(x =>
                    (x.Row == square.Row + 1 || x.Row == square.Row - 1) &&
                    (x.Column == square.Column + 1 || x.Column == square.Column - 1));

                square.DiagonalNeighbours = (IEnumerable<ISquare>)neighbours;
            }
        }
    }
}
