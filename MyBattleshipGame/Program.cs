using MyBattleshipGame.GameConfig;
using System;
using System.Runtime.InteropServices;

namespace MyBattleshipGame
{
    internal class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int Hide = 0;
        private const int Maximize = 3;
        private const int Minimize = 6;
        private const int Restore = 9;

        private static void Main(string[] args)
        {
            ShowWindow(ThisConsole, Maximize);

            Console.WriteLine("State your name Sir.");
            var playerName = Console.ReadLine();

            var config = new SimpleGameConfig();
            var game = new SinglePlayerGame(config, playerName);

            while (!game.IsFinished)
            {
                game.Continue();
            }

            Console.Read();
        }
    }
}
