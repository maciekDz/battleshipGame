using System.Collections.Generic;
using MyBattleshipGame.Models;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface ISquareProvider<out T> where T : ISquare
    {
        IEnumerable<T> GetSquares((int rows, int columns) boardSize);
    }
}
