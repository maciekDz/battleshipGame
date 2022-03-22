using MyBattleshipGame.GameConfig;
using MyBattleshipGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBattleshipGame
{
    public interface IGame
    {
        IList<Player> Players { get; set; }

        IList<string> Notifications { get; set; }

        bool IsFinished { get; }

        void Continue();
    }

    public class SinglePlayerGame : IGame
    {
        private readonly IGameConfig _gameConfig;
        private readonly string _playerName;

        public IList<string> Notifications { get; set; }

        public bool IsFinished { get; private set; }

        public IList<Player> Players { get; set; }

        public SinglePlayerGame(IGameConfig gameConfig, string playerName)
        {
            _gameConfig = gameConfig;
            _playerName = playerName;
            Notifications = new List<string>();

            CreatePlayers();
        }

        public void Continue()
        {
            PrintGameState();

            foreach (var player in Players)
            {
                var opponent = Players.FirstOrDefault(x => x != player);
                var coordinates = player.GetCoordinates();
                var hit = opponent?.Territory.FirstOrDefault(x => string.Equals(x.Address, coordinates, StringComparison.CurrentCultureIgnoreCase));

                if (hit == null) continue;
                {
                    hit.IsHit = true;
                    var hitShip = opponent.Fleet.FirstOrDefault(x => x.Masts.Contains(hit));

                    if (hitShip == null) continue;
                    {
                        var title = (opponent.PlayerType == PlayerType.Human ? "Our" : $"{opponent.Name}'s");
                        Notifications.Add($"{title} {hit.Name} has been hit");

                        if (hitShip.Masts.All(x => x.IsHit))
                        {
                            Notifications.Add($"{title}'s {hit.Name} has been sunken!");
                        }
                    }
                }

                CheckForWinner();
            }
        }

        private void CheckForWinner()
        {
            var playerWhoHaveAllShipsSunken = Players.FirstOrDefault(x => x.Fleet.All(y => y.Masts.All(z => z.IsHit)));
            if (playerWhoHaveAllShipsSunken == null) return;
            {
                IsFinished = true;

                var winner = Players.FirstOrDefault(x => x != playerWhoHaveAllShipsSunken);

                Notifications.Clear();

                var announcement = winner?.PlayerType == PlayerType.Human ?
                    "Congratulations Sir! We won." :
                    $"I'm sorry Sir but {winner?.Name} has defeated us.";

                Notifications.Add(announcement);
                Notifications.Add("Game over!");

                PrintGameState();
            }
        }

        private void PrintGameState()
        {
            Console.Clear();

            _gameConfig.GamePrintServices.PrintGame(this);
            _gameConfig.GamePrintServices.PrintNotifications(Notifications);

            Notifications.Clear();
        }

        private void CreatePlayers()
        {
            var playerNames = new List<(PlayerType playerType, string name)>()
            {
                (PlayerType.Human, _playerName),
                (PlayerType.Computer, "Enemy")
            };

            Players = _gameConfig.PlayerFactory.CreatePlayers(playerNames, _gameConfig.FleetBuilder);
        }
    }
}
