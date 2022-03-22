using MyBattleshipGame.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services;

namespace MyBattleshipGame.Models
{
    public enum PlayerType
    {
        Human,
        Computer
    }

    public abstract class Player
    {
        public string Name { get; set; }

        public IEnumerable<ISquare> Territory { get; set; }

        public IList<string> OpponentsTerritory => Territory.Select(x => x.Address).ToList();

        public IEnumerable<IShip> Fleet { get; set; }

        public PlayerType PlayerType { get; set; }

        protected Player(string name, IBoard board, IFleetBuilder<IShip> fleetBuilder)
        {
            Name = name;
            Territory = board.Squares;
            Fleet = fleetBuilder.BuildFleet(Territory);
        }

        public abstract string GetCoordinates();
    }

    public class HumanPlayer : Player
    {
        private readonly ICoordinatesPicker _coordinatesPicker;

        public HumanPlayer(string name, IBoard board, IFleetBuilder<IShip> fleetBuilder)
            : base(name, board, fleetBuilder)
        {
            PlayerType = PlayerType.Human;
            Territory.ToList().ForEach(x => x.HideShip = false);
            _coordinatesPicker = new ManualCoordinatesPicker(OpponentsTerritory);
        }

        public override string GetCoordinates()
        {
            return _coordinatesPicker.GetCoordinates();
        }
    }

    public class ComputerPlayer : Player
    {
        private readonly ICoordinatesPicker _coordinatesPicker;

        public ComputerPlayer(string name, IBoard board, IFleetBuilder<IShip> fleetBuilder)
        : base(name, board, fleetBuilder)
        {
            PlayerType = PlayerType.Computer;
            Territory.ToList().ForEach(x => x.HideShip = true);
            _coordinatesPicker = new RandomCoordinatesPicker(OpponentsTerritory);
        }

        public override string GetCoordinates()
        {
            return _coordinatesPicker.GetCoordinates();
        }
    }
}
