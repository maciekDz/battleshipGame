using System;
using System.Collections.Generic;
using MyBattleshipGame.Services;
using MyBattleshipGame.Tests.TestModels;
using NUnit.Framework;

namespace MyBattleshipGame.Tests
{
    [TestFixture]
    public class BasicStyleProviderTests
    {
        [TestCaseSource(nameof(Elements))]
        public void WrapElement_ReturnsExpectedString(string element)
        {
            var styleProvider = new BasicStyleProvider();

            var wrappedElement = styleProvider.WrapElement(element);

            Assert.AreEqual($"[{element}]", wrappedElement);
        }

        [Test]
        public void ApplyColumnHeaderStyle_AfterExecution_LeavesConsoleColorUnchanged()
        {
            var originalColor = Console.ForegroundColor;

            var styleProvider = new BasicStyleProvider();

            void PrintColumnsAction()
            {
                Console.WriteLine("test");
            }

            styleProvider.ApplyColumnHeadersStyle(PrintColumnsAction);

            Assert.AreEqual(originalColor, Console.ForegroundColor);
        }

        [Test]
        public void ApplyNotificationStyle_AfterExecution_LeavesConsoleColorUnchanged()
        {
            var originalColor = Console.ForegroundColor;

            var styleProvider = new BasicStyleProvider();

            void PrintNotificationsAction()
            {
                Console.WriteLine("test");
            }

            styleProvider.ApplyNotificationsStyle(PrintNotificationsAction);

            Assert.AreEqual(originalColor, Console.ForegroundColor);
        }

        [Test]
        public void ApplySquareStyle_AfterExecution_LeavesConsoleColorUnchanged()
        {
            var originalColor = Console.ForegroundColor;

            var styleProvider = new BasicStyleProvider();

            var square = new TestSquare();

            styleProvider.ApplySquareStyle(square);

            Assert.AreEqual(originalColor, Console.ForegroundColor);
        }

        [Test]
        public void ApplyRowHeaderStyle_AfterExecution_LeavesConsoleColorUnchanged()
        {
            var originalColor = Console.ForegroundColor;

            var styleProvider = new BasicStyleProvider();

            void PrintRowAction()
            {
                Console.WriteLine("test");
            }

            styleProvider.ApplyRowHeaderStyle(PrintRowAction);

            Assert.AreEqual(originalColor, Console.ForegroundColor);
        }

        private static IEnumerable<TestCaseData> Elements
        {
            get
            {
                yield return new TestCaseData("test");
                yield return new TestCaseData("name");
                yield return new TestCaseData("~");
                yield return new TestCaseData("x");
                yield return new TestCaseData("1");
            }
        }
    }
}
