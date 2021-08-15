using Moq;
using Services.Interfaces;
using System;
using Xunit;

namespace ConsoleApp.Tests.Unit
{
    public class PokerAppTests
    {
        [Fact]
        public void StartFileRead_Should()
        {
            var pokerService = new Mock<IPokerService>();
            var pokerApp = new PokerApp(pokerService.Object);

            pokerApp.StartFileRead("..\\TestData\\ValidPokerHands.txt");
            string actual = Console.ReadLine();
            Assert.Equal("3H JS 3C 7C 5D => One pair", actual);
        }
    }
}
