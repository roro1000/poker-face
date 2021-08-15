using Common.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class PokerService : IPokerService
    {
        private readonly List<string> Names = new List<string>
        {
            "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"
        };

        private readonly List<string> Suits = new List<string>
        {
            "H", "D", "S", "C"
        };

        public string GetHandType(string[] cards)
        {
            if (cards.Length != 5)
            {
                throw new ArgumentException("Invalid number of cards in hand.");
            }

            var hand = new List<Card>();
            foreach (var card in cards)
            {              
                hand.Add(CreateCard(card));
            }
            return GetPairs(hand) ?? GetFlushOrStraight(hand) ?? "High card";
        }

        private Card CreateCard(string card)
        {
            if (card.Length != 2)
            {
                throw new ArgumentException($"Invalid card: { card }");
            }

            var name = card.Substring(0, 1).ToUpper();
            var suit = card.Substring(1, 1).ToUpper();

            if (!Names.Contains(name))
            {
                throw new ArgumentException($"Invalid card name: { card }");
            }
            if (!Suits.Contains(suit))
            {
                throw new ArgumentException($"Invalid card suit: { card }");
            }

            return new Card { Name = name, Suit = suit };
        }

        private string GetPairs(List<Card> hand)
        {
            var nameGroups = hand.GroupBy(c => c.Name)
                .OrderByDescending(g => g.Count())
                .Where(g => g.Count() > 1)
                .Select(c => c)
                .ToList();

            if (nameGroups.Count == 2)
            {
                if (nameGroups[0].Count() == 3)
                {
                    return "Full House";
                }
                return "Two pair";
            }

            if (nameGroups.Count == 1)
            {
                return (nameGroups[0].Count()) switch
                {
                    2 => "One pair",
                    3 => "Three of a kind",
                    4 => "Four of a kind",
                    _ => null,
                };
            }
            return null;
        }

        private string GetFlushOrStraight(List<Card> hand)
        {
            var handNames = hand.Select(c => c.Name);
            var orderedHand = Names.Intersect(handNames).ToList();
            var suits = hand.GroupBy(c => c.Suit)
                .OrderBy(g => g.Count())
                .Where(g => g.Count() == 5)
                .Select(c => c)
                .ToList();

            var isAceHigh = IsAceHighStraightHand(orderedHand);
            var isSequential = IsSequentialHand(orderedHand);
            var singleSuit = suits.Count == 1;

            if (isAceHigh)
            {
                return singleSuit ? "Royal Flush" : "Straight";
            }

            if (isSequential)
            {
                return singleSuit ? "Straight flush" : "Straight";
            }

            return singleSuit ? "Flush" : null;
        }

        private bool IsSequentialHand(List<string> orderedHand)
        {
            var firstIndex = Names.IndexOf(orderedHand[0]);

            // Five high straight OR any 5 seqential card names.
            return orderedHand[4] == "A" && orderedHand[3] == "5" 
                || orderedHand[4] == Names[firstIndex + 4]; ;
        }

        private bool IsAceHighStraightHand(List<string> orderedHand)
        {
            return orderedHand[0] == "T" && orderedHand[4] == "A";
        }
    }
}
