using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Models;

namespace MyBattleshipGame.Tests.TestModels
{
    public class TestSquare : ISquare
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

        public TestSquare()
        {
            SquareType = SquareType.Water;
        }
    }
}
