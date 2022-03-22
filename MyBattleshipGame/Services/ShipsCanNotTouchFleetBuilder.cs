using System;
using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class ShipsCanNotTouchFleetBuilder<T> : IFleetBuilder<T> where T : IShip, new()

    {
        private readonly (int shipSize, int numOfShips, string shipName)[] _fleetDetails;

        public ShipsCanNotTouchFleetBuilder((int shipSize, int numOfShips, string shipName)[] fleetDetails)
        {
            _fleetDetails = fleetDetails;
        }

        public IEnumerable<T> BuildFleet(IEnumerable<ISquare> territory)
        {
            IEnumerable<T> fleet = null;

            var counter = 0;

            while (fleet == null && counter <= 1000)
            {
                fleet = TryBuildFleet(territory);
                counter++;
            }

            if (fleet == null)
            {
                throw new InvalidOperationException("fleet too big");
            }

            return fleet;
        }

        private IEnumerable<T> TryBuildFleet(IEnumerable<ISquare> territory)
        {
            var fleet = new List<T>();

            foreach (var (shipSize, numOfShips, shipName) in _fleetDetails)
            {
                for (var i = 1; i <= numOfShips; i++)
                {
                    T ship = default(T);
                    var counter = 0;
                    while (ship == null && counter <= 100)
                    {
                        ship = TryBuildShip(territory, shipSize, shipName);
                        counter++;
                    }

                    if (ship == null)
                    {
                        return null;
                    }

                    fleet.Add(ship);
                }
            }

            return fleet;
        }

        private static T TryBuildShip(IEnumerable<ISquare> territory, int shipSize, string shipName)
        {
            var ship = new T();
            for (var j = 1; j <= shipSize; j++)
            {
                var potentialMasts = FindMastCandidates(territory, ship);

                if (!potentialMasts.Any())
                {
                    WreckShip(ship);
                    return default(T);
                }

                var availableRows = potentialMasts.Select(x => x.Row).Distinct();
                var randomRow = availableRows.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                var availableColumns = potentialMasts.Where(x => x.Row == randomRow).Select(x => x.Column).Distinct();
                var randomColumn = availableColumns.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                var mast = territory.FirstOrDefault(x => x.Row == randomRow && x.Column == randomColumn);

                mast.SquareType = SquareType.Ship;

                ship.AddMast(mast);
            }

            ship.Masts.ForEach(x => x.Name = shipName);

            return ship;
        }

        private static IEnumerable<ISquare> FindMastCandidates(IEnumerable<ISquare> territory, T ship)
        {
            var availableTerritory = territory.Where(x => x.SquareType != SquareType.Ship);

            if (!ship.Masts.Any())
            {
                return availableTerritory.Where(x => x.AllNeighbours.All(y => y.SquareType != SquareType.Ship));
            }

            var shipSurroundings = ship.Masts.SelectMany(x => x.PerpendicularNeighbours);
            availableTerritory = availableTerritory.Where(x => shipSurroundings.Contains(x));

            var candidates = new List<ISquare>();
            foreach (var candidate in availableTerritory)
            {
                var foundMasts = candidate.AllNeighbours.Where(x => x.SquareType == SquareType.Ship);
                if (foundMasts.All(x => ship.Masts.Contains(x)))
                {
                    candidates.Add(candidate);
                }
            }

            return candidates;
        }

        private static void WreckShip(T ship)
        {
            ship.Masts.ForEach(x => x.SquareType = SquareType.Water);
        }
    }
}