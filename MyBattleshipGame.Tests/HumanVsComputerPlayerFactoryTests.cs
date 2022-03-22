using System.Collections.Generic;
using System.Linq;
using Moq;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services;
using MyBattleshipGame.Services.Abstraction;
using MyBattleshipGame.Tests.TestModels;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class HumanVsComputerPlayerFactoryTests
    {
        [Test]
        public void CreatePlayers_ReturnsTwoPlayers()
        {
            var fleetBuilderMock = new Mock<IFleetBuilder<IShip>>();

            var playerTypes = new List<(PlayerType playerType, string name)>()
            {
                (PlayerType.Human,"abc"),
                (PlayerType.Computer,"cba")
            };

            var factory = new HumanVsComputerPlayerFactory<TestBoard>();

            var players = factory.CreatePlayers(playerTypes, fleetBuilderMock.Object);

            Assert.AreEqual(2, players.Count);
        }

        [Test]
        public void CreatePlayers_ReturnsExpectedTypesOfPlayers()
        {
            var fleetBuilderMock = new Mock<IFleetBuilder<IShip>>();

            var playerTypes = new List<(PlayerType playerType, string name)>()
            {
                (PlayerType.Human,"abc"),
                (PlayerType.Computer,"cba")
            };

            var factory = new HumanVsComputerPlayerFactory<TestBoard>();

            var players = factory.CreatePlayers(playerTypes, fleetBuilderMock.Object);

            Assert.That(players.OfType<HumanPlayer>().Count() == 1);
            Assert.That(players.OfType<ComputerPlayer>().Count() == 1);
        }
    }
}
