using MyBattleshipGame.Models;
using MyBattleshipGame.Services;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.GameConfig
{
    public interface IGameConfig
    {
        IFleetBuilder<IShip> FleetBuilder { get; }

        IGamePrintServices GamePrintServices { get; }

        IPlayerFactory PlayerFactory { get; }
    }

    public class SimpleGameConfig : IGameConfig
    {
        //public IFleetBuilder<IShip> ShipsCanTouchFleetBuilder => new ShipsCanTouchFleetBuilder<Ship>(new[] { (5, 1, "Battleship"), (4, 1, "Destroyer"), (3, 1, "Carrier"), (3, 1, "Patrol Boat"), (2, 1, "Submarine") });
        public IFleetBuilder<IShip> FleetBuilder => new ShipsCanTouchFleetBuilder<Ship>(new[] { (5, 1, "Battleship"), (4, 2, "Destroyer") });

        public IGamePrintServices GamePrintServices => new VerticalPrintServices<BasicStyleProvider>();

        public IPlayerFactory PlayerFactory => new HumanVsComputerPlayerFactory<Board10X10>();
    }
}
