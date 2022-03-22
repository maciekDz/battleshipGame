using System.Collections.Generic;
using System.Linq;

namespace MyBattleshipGame.Models
{
    public enum SquareType
    {
        Water,
        Ship
    }

    public interface ISquare
    {
        string Name { get; set; }

        int Row { get; set; }

        int Column { get; set; }

        string Address { get; }

        SquareType SquareType { get; set; }

        bool IsHit { get; set; }

        bool HideShip { get; set; }

        IEnumerable<ISquare> PerpendicularNeighbours { get; set; }

        IEnumerable<ISquare> DiagonalNeighbours { get; set; }

        IEnumerable<ISquare> AllNeighbours { get; }
    }

    public class Square : ISquare
    {
        public string Name { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Address => $"{(char)(Column + 64)}{Row}";

        public SquareType SquareType { get; set; }

        public bool IsHit { get; set; }

        public bool HideShip { get; set; }

        public IEnumerable<ISquare> PerpendicularNeighbours { get; set; }

        public IEnumerable<ISquare> DiagonalNeighbours { get; set; }

        public IEnumerable<ISquare> AllNeighbours => PerpendicularNeighbours.Union(DiagonalNeighbours);

        public Square()
        {
            SquareType = SquareType.Water;
        }
    }
}
