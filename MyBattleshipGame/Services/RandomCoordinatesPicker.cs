using System;
using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services.Abstraction;

namespace MyBattleshipGame.Services
{
    public class RandomCoordinatesPicker : ICoordinatesPicker
    {
        private readonly IList<string> _territory;

        public RandomCoordinatesPicker(IList<string> territory)
        {
            _territory = territory;
        }

        public string GetCoordinates(Func<string> overrideDefault = null)
        {
            var result= _territory.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            _territory.Remove(result);

            return result;
        }
    }
}
