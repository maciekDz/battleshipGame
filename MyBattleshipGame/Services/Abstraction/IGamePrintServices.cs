using System.Collections.Generic;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface IGamePrintServices
    {
        void PrintGame(IGame game);

        void PrintNotifications(IList<string> notifications);
    }
}
