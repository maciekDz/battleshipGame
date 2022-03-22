using MyBattleshipGame.Models;
using MyBattleshipGame.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBattleshipGame.Services
{
    public class VerticalPrintServices<T> : IGamePrintServices where T : IStyleProvider, new()
    {
        private readonly IStyleProvider _styleProvider;
        private int _maxColumn;
        private int _minColumn;
        private int _maxRow;
        private int _minRow;

        public VerticalPrintServices()
        {
            _styleProvider = new T();
        }

        public void PrintGame(IGame game)
        {
            SetBoardSize(game);

            Console.Clear();

            foreach (var player in game.Players)
            {
                PrintTerritory($"{player.Name}'s Fleet:", player.Territory);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void SetBoardSize(IGame game)
        {
            var territory = game.Players.FirstOrDefault()?.Territory.ToList();
            if (territory == null)
            {
                throw new NullReferenceException();
            }

            _maxColumn = territory.Max(x => x.Column);
            _minColumn = territory.Min(x => x.Column);
            _maxRow = territory.Max(x => x.Row);
            _minRow = territory.Min(x => x.Row);
        }

        public void PrintNotifications(IList<string> notifications)
        {
            _styleProvider.ApplyNotificationsStyle(() =>
            {
                if (!notifications.Any()) return;
                notifications.ToList().ForEach(Console.WriteLine);
                Console.WriteLine();
            });
        }

        private void PrintTerritory(string title, IEnumerable<ISquare> territory)
        {
            Console.WriteLine(title);
            PrintColumnHeader();
            for (var i = _minRow; i <= _maxRow; i++)
            {
                Console.WriteLine();
                PrintRowHeader(i);
                for (var j = _minColumn; j <= _maxColumn; j++)
                {
                    var square = territory.FirstOrDefault(x => x.Row == i && x.Column == j);
                    _styleProvider.ApplySquareStyle(square);
                }
            }
        }

        private void PrintColumnHeader()
        {
            _styleProvider.ApplyColumnHeadersStyle(() =>
            {
                var offset = FillToMaxRowNumberLenght(string.Empty);
                Console.Write(_styleProvider.WrapElement(offset));
                var columns = _maxColumn - _minColumn + 1;
                for (var i = 1; i <= columns; i++)
                {
                    Console.Write(_styleProvider.WrapElement(((char)(i + 64)).ToString()));
                }
            });
        }

        private void PrintRowHeader(int row)
        {
            var rowHeader = FillToMaxRowNumberLenght(row.ToString());
            _styleProvider.ApplyRowHeaderStyle(() => Console.Write(_styleProvider.WrapElement(rowHeader)));
        }

        private string FillToMaxRowNumberLenght(string content)
        {
            var maxRowNumberLenght = _maxRow.ToString().Length;
            var contentLength = content.Length;
            var extraSpaces = maxRowNumberLenght - contentLength;

            var result = new StringBuilder($"{content}");
            for (var i = 1; i <= extraSpaces; i++)
            {
                result.Append(" ");
            }

            return result.ToString();
        }
    }
}
