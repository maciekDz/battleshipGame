using MyBattleshipGame.Models;
using System.Collections.Generic;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface IFleetBuilder<out T> where T : IShip
    {
        IEnumerable<T> BuildFleet(IEnumerable<ISquare> territory);
    }
}