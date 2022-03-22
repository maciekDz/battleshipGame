using System.Collections.Generic;
using MyBattleshipGame.Models;

namespace MyBattleshipGame.Tests.TestModels
{
  public class TestShip : IShip
    {
        public List<ISquare> Masts { get; } = new List<ISquare>();
        public void AddMast(ISquare mast)
        {
            Masts.Add(mast);
        }
    }
}