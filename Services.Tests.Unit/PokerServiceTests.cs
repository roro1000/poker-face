using Services.Interfaces;
using System;
using Xunit;

namespace Services.Tests.Unit
{
    public class PokerServiceTests
    {
        private IPokerService _pokerService;
        public PokerServiceTests()
        {
            _pokerService = new PokerService();
        }

        [Fact]
        public void GetHandType_ShouldThrowExceptionForInvalidCardName()
        {
            var cards = new string[] { "1D", "4H", "8D", "QC", "TD" };
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                _pokerService.GetHandType(cards);
            });
            Assert.Equal("Invalid card name: 1D", exception.Message);
        }

        [Fact]
        public void GetHandType_ShouldThrowExceptionForInvalidCardSuit()
        {
            var cards = new string[] { "2A", "4H", "8D", "QC", "TD" };
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                _pokerService.GetHandType(cards);
            });

            Assert.Equal("Invalid card suit: 2A", exception.Message);
        }

        [Fact]
        public void GetHandType_ShouldThrowExceptionForLessThan5Cards()
        {
            var cards = new string[] { "2A", "4H" };
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                _pokerService.GetHandType(cards);
            });

            Assert.Equal("Invalid number of cards in hand.", exception.Message);
        }

        [Fact]
        public void GetHandType_ShouldThrowExceptionForMoreThan5Cards()
        {
            var cards = new string[] { "2A", "4H", "8D", "QC", "TD", "3H" };
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                _pokerService.GetHandType(cards);
            });

            Assert.Equal("Invalid number of cards in hand.", exception.Message);
        }

        [Fact]
        public void GetHandType_ShouldThrowExceptionForInvalidCard()
        {
            var cards = new string[] { "2", "4H", "8D", "QC", "TD" };
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                _pokerService.GetHandType(cards);
            });

            Assert.Equal("Invalid card: 2", exception.Message);
        }

        [Fact]
        public void GetHandType_ShouldReturnFullHouse()
        {
            var cards = new string[] { "2D", "2H", "8D", "8C", "8H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Full House", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnTwoPair()
        {
            var cards = new string[] { "2D", "2H", "8D", "8C", "9H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Two pair", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnOnePair()
        {
            var cards = new string[] { "2D", "2H", "8D", "TC", "9H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("One pair", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnThreeOfAKind()
        {
            var cards = new string[] { "2D", "2H", "2D", "TC", "9H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Three of a kind", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnFourOfAKind()
        {
            var cards = new string[] { "2D", "2H", "2D", "2C", "9H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Four of a kind", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnHighCard()
        {
            var cards = new string[] { "2D", "3H", "TD", "8C", "9H" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("High card", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnRoyalFlush()
        {
            var cards = new string[] { "AD", "QD", "JD", "KD", "TD" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Royal Flush", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnStraightForAceHigh()
        {
            var cards = new string[] { "AD", "QC", "JD", "KH", "TD" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Straight", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnStraightForFiveHigh()
        {
            var cards = new string[] { "2D", "4D", "3D", "5C", "AH" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Straight", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnStraightFlushForFiveHigh()
        {
            var cards = new string[] { "2D", "4D", "3D", "5D", "AD" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Straight flush", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnStraight()
        {
            var cards = new string[] { "4S", "6D", "3H", "5C", "7D" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Straight", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnStraightFlush()
        {
            var cards = new string[] { "4D", "6D", "3D", "5D", "7D" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Straight flush", handType);
        }

        [Fact]
        public void GetHandType_ShouldReturnFlush()
        {
            var cards = new string[] { "2D", "3D", "7D", "JD", "TD" };
            var handType = _pokerService.GetHandType(cards);
            Assert.Equal("Flush", handType);
        }
    }
}
