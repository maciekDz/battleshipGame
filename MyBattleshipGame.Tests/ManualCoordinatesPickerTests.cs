using System.Collections.Generic;
using System.Linq;
using MyBattleshipGame.Services;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class ManualCoordinatesPickerTests
    {
        [TestCaseSource(nameof(Territory))]
        public void GetCoordinates_ReturnsElementFromTerritory_WhenConsoleReadLineIsOverridden(List<string> territory)
        {
            var originalTerritory = new List<string>(territory);
            var coordinatesPicker = new ManualCoordinatesPicker(territory);

            var coordinates = coordinatesPicker.GetCoordinates(() => territory.FirstOrDefault());

            Assert.That(originalTerritory.Contains(coordinates));
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
