using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class HumanVsComputerPlayerFactory<T> : IPlayerFactory where T:IBoard, new()
    {
        public IList<Player> CreatePlayers(IList<(PlayerType playerType, string name)> playerNames,  IFleetBuilder<IShip> fleetBuilder)
        {
            var players = new List<Player>();

            var humanName = playerNames.Where(x => x.playerType == PlayerType.Human).Select(x => x.name).FirstOrDefault();
            var computerName = playerNames.Where(x => x.playerType == PlayerType.Computer).Select(x => x.name).FirstOrDefault();

            var human = new HumanPlayer(humanName,
                new T(),
                fleetBuilder);

            var computer = new ComputerPlayer(computerName,
                new T(),
                fleetBuilder);

            players.Add(human);
            players.Add(computer);

            return players;
        }
    }
}
