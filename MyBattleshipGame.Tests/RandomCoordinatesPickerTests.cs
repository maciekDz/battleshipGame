using System.Collections.Generic;
using MyBattleshipGame.Services;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class RandomCoordinatesPickerTests
    {
        [TestCaseSource(nameof(Territory))]
        public void GetCoordinates_ReturnsSquareAddress_FromGivenTerritory(List<string> territory)
        {
            var originalTerritory = new List<string>(territory);

            var coordinatesPicker = new RandomCoordinatesPicker(territory);

            var coordinates = coordinatesPicker.GetCoordinates();

            Assert.That(originalTerritory.Contains(coordinates));
        }

        [TestCaseSource(nameof(Territory))]
        public void GetCoordinates_RemovesSquareAddressFromTerritory_AfterItsPicked(List<string> territory)
        {
            var coordinatesPicker = new RandomCoordinatesPicker(territory);

            var coordinates = coordinatesPicker.GetCoordinates();

            Assert.That(!territory.Contains(coordinates));
        }

        [TestCaseSource(nameof(Territory))]
        public void GetCoordinates_ReturnsNull_AfterPickingAllGivenValues(List<string> territory)
        {
            var originalTerritory = new List<string>(territory);

            var coordinatesPicker = new RandomCoordinatesPicker(territory);

            string coordinates;
            var counter = 0;
            do
            {
                counter++;
                coordinates = coordinatesPicker.GetCoordinates();

            } while (coordinates != null);

            Assert.AreEqual(originalTerritory.Count, counter - 1);
        }

        private static IEnumerable<TestCaseData> Territory
        {
            get
            {
                yield return new TestCaseData(new List<string>() { "A1", "A2", "A3" });
                yield return new TestCaseData(new List<string>() { "x", "y", "z" });
                yield return new TestCaseData(new List<string>() { "A1", "A2", "A3", "A4", "A5", "A6" });
                yield return new TestCaseData(new List<string>() { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A27", "x", "test", "sdc", "n12" });
            }
        }
    }
}
