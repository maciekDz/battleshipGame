using System;
using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class ManualCoordinatesPicker : ICoordinatesPicker
    {
        private readonly IList<string> _territory;

        public ManualCoordinatesPicker(IList<string> territory)
        {
            _territory = territory;
        }

        public string GetCoordinates(Func<string> overrideDefault = null)
        {
            var actualInput = overrideDefault ?? Console.ReadLine;
            string input;
            string coordinates = null;
            var isValid = false;
            do
            {
                Console.WriteLine("What are the attack coordinates Sir?");
                input = actualInput.Invoke();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("These are not enemy's Coordinates Sir!");
                    Console.WriteLine();

                    continue;
                }

                coordinates = _territory.FirstOrDefault(x => string.Equals(x, input, StringComparison.CurrentCultureIgnoreCase));
                if (!_territory.Contains(coordinates))
                {
                    Console.WriteLine($"{input?.ToUpper()} are not enemy's Coordinates Sir!");
                    Console.WriteLine();

                    continue;
                }

                isValid = true;

            } while (!isValid);

            _territory.Remove(coordinates);

            return coordinates;
        }
    }
}
