using System.Collections.Generic;
using MyBattleshipGame.Models;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface IPlayerFactory
    {
        IList<Player> CreatePlayers(IList<(PlayerType playerType, string name)> playerNames, IFleetBuilder<IShip> fleetBuilder);
    }
}