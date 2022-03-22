using System;
using MyBattleshipGame.Models;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class BasicStyleProvider : IStyleProvider
    {
        public void ApplyColumnHeadersStyle(Action printColumnHeaders)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = GetColumnHeaderColor();

            printColumnHeaders.Invoke();

            Console.ForegroundColor = originalColor;
        }

        public void ApplyRowHeaderStyle(Action printRowHeader)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = GetRowHeaderColor();

            printRowHeader.Invoke();

            Console.ForegroundColor = originalColor;
        }

        public void ApplyNotificationsStyle(Action printNotifications)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = GetNotificationsColor();

            printNotifications.Invoke();

            Console.ForegroundColor = originalColor;
        }

        public void ApplySquareStyle(ISquare square)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetSquareColor(square);
            Console.Write(GetSquareSymbol(square));
            Console.ForegroundColor = originalColor;
        }

        public string WrapElement(string element)
        {
            return $"[{element}]";
        }

        private static ConsoleColor GetColumnHeaderColor()
        {
            return ConsoleColor.DarkYellow;
        }

        private static ConsoleColor GetRowHeaderColor()
        {
            return ConsoleColor.DarkYellow;
        }

        private static ConsoleColor GetNotificationsColor()
        {
            return ConsoleColor.Yellow;
        }

        private static ConsoleColor GetSquareColor(ISquare square)
        {
            switch (square.SquareType)
            {
                case SquareType.Ship when square.IsHit:
                    return ConsoleColor.Red;
                case SquareType.Ship when !square.HideShip:
                    return ConsoleColor.Green;
                case SquareType.Water when square.IsHit:
                    return ConsoleColor.Cyan;
                default:
                    return ConsoleColor.Blue;
            }
        }

        private string GetSquareSymbol(ISquare square)
        {
            switch (square.SquareType)
            {
                case SquareType.Ship when square.IsHit || !square.HideShip:
                    return WrapElement(square.Name[0].ToString());
                case SquareType.Water when square.IsHit:
                    return WrapElement("x");
                default:
                    return WrapElement("~");
            }
        }
    }
}
