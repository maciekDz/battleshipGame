using System;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface ICoordinatesPicker
    {
        string GetCoordinates(Func<string> overrideDefault = null);
    }
}
