using System.Collections.Generic;

namespace MyBattleshipGame.Models
{
    public interface IShip
    {
        List<ISquare> Masts { get; }
        void AddMast(ISquare mast);
    }

    public class Ship : IShip
    {
        public List<ISquare> Masts { get; } = new List<ISquare>();

        public void AddMast(ISquare mast)
        {
            Masts.Add(mast);
        }
    }
}