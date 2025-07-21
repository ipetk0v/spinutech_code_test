using SpinutechCodeTest.Constants;

namespace SpinutechCodeTest.Services
{
    public class PokerHandService : IPokerHandService
    {
        public enum Suit { Hearts, Diamonds, Clubs, Spades }
        public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

        public class Card
        {
            public Suit Suit { get; set; }
            public Rank Rank { get; set; }
            
            public Card(Suit suit, Rank rank)
            {
                Suit = suit;
                Rank = rank;
            }
        }

        public enum HandRank
        {
            HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, 
            FullHouse, FourOfAKind, StraightFlush, RoyalFlush
        }

        public string EvaluateHand(string handInput)
        {
            try
            {
                var hand = ParseHand(handInput);
                var rank = EvaluateHandRank(hand);
                return GetHandDescription(handInput);
            }
            catch (Exception ex)
            {
                return AppConstants.PokerHand.Error + ex.Message;
            }
        }

        public string GetHandDescription(string handInput)
        {
            try
            {
                var hand = ParseHand(handInput);
                var rank = EvaluateHandRank(hand);
                var groups = hand.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
                
                switch (rank)
                {
                    case HandRank.RoyalFlush: return AppConstants.PokerHand.RoyalFlush;
                    case HandRank.StraightFlush: return AppConstants.PokerHand.StraightFlush;
                    case HandRank.FourOfAKind: return AppConstants.PokerHand.FourOfAKind + $"({groups[0].Key})";
                    case HandRank.FullHouse: return AppConstants.PokerHand.FullHouse + $"({groups[0].Key}s" + AppConstants.PokerHand.Over + $"{groups[1].Key}s)";
                    case HandRank.Flush: return AppConstants.PokerHand.Flush;
                    case HandRank.Straight: return AppConstants.PokerHand.Straight;
                    case HandRank.ThreeOfAKind: return AppConstants.PokerHand.ThreeOfAKind + $"({groups[0].Key}s)";
                    case HandRank.TwoPair: return AppConstants.PokerHand.TwoPair + $"({groups[0].Key}s" + AppConstants.PokerHand.And + $"{groups[1].Key}s)";
                    case HandRank.Pair: return AppConstants.PokerHand.Pair + $" {groups[0].Key}s";
                    default: return AppConstants.PokerHand.HighCard + $"({groups[0].Key})";
                }
            }
            catch (Exception ex)
            {
                return AppConstants.PokerHand.Error + ex.Message;
            }
        }

        private List<Card> ParseHand(string handInput)
        {
            var cards = new List<Card>();
            var parts = handInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length != 5)
                throw new ArgumentException(AppConstants.PokerHand.HandMustContainExactly5Cards);

            foreach (var part in parts)
            {
                if (part.Length != 2 && part.Length != 3)
                    throw new ArgumentException(AppConstants.PokerHand.InvalidCardFormat + part);

                Rank rank;
                Suit suit;

                if (part.Length == 3)
                {
                    // Handle 10 (Ten) - format: "10c", "10h", etc.
                    if (part.StartsWith("10", StringComparison.OrdinalIgnoreCase))
                    {
                        rank = Rank.Ten;
                        suit = ParseSuit(part[2]);
                    }
                    else
                    {
                        throw new ArgumentException(AppConstants.PokerHand.InvalidCardFormat + part);
                    }
                }
                else
                {
                    // Handle other cards - format: "Ah", "Ks", etc.
                    rank = ParseRank(part[0]);
                    suit = ParseSuit(part[1]);
                }

                cards.Add(new Card(suit, rank));
            }

            return cards;
        }

        private Rank ParseRank(char rankChar)
        {
            return rankChar switch
            {
                '2' => Rank.Two,
                '3' => Rank.Three,
                '4' => Rank.Four,
                '5' => Rank.Five,
                '6' => Rank.Six,
                '7' => Rank.Seven,
                '8' => Rank.Eight,
                '9' => Rank.Nine,
                'T' or 't' => Rank.Ten,
                'J' or 'j' => Rank.Jack,
                'Q' or 'q' => Rank.Queen,
                'K' or 'k' => Rank.King,
                'A' or 'a' => Rank.Ace,
                _ => throw new ArgumentException(AppConstants.PokerHand.InvalidRank + rankChar)
            };
        }

        private Suit ParseSuit(char suitChar)
        {
            return suitChar switch
            {
                'H' or 'h' => Suit.Hearts,
                'D' or 'd' => Suit.Diamonds,
                'C' or 'c' => Suit.Clubs,
                'S' or 's' => Suit.Spades,
                _ => throw new ArgumentException(AppConstants.PokerHand.InvalidSuit + suitChar)
            };
        }

        private HandRank EvaluateHandRank(List<Card> hand)
        {
            if (hand.Count != 5) throw new ArgumentException("Hand must contain exactly 5 cards");
            
            var groups = hand.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
            var suits = hand.Select(c => c.Suit).Distinct().Count();
            var ranks = hand.Select(c => (int)c.Rank).OrderBy(r => r).ToList();
            
            // Check for flush
            bool isFlush = suits == 1;
            
            // Check for straight
            bool isStraight = IsStraight(ranks);
            
            // Royal Flush
            if (isFlush && isStraight && ranks[0] == 10) return HandRank.RoyalFlush;
            
            // Straight Flush
            if (isFlush && isStraight) return HandRank.StraightFlush;
            
            // Four of a Kind
            if (groups[0].Count() == 4) return HandRank.FourOfAKind;
            
            // Full House
            if (groups[0].Count() == 3 && groups[1].Count() == 2) return HandRank.FullHouse;
            
            // Flush
            if (isFlush) return HandRank.Flush;
            
            // Straight
            if (isStraight) return HandRank.Straight;
            
            // Three of a Kind
            if (groups[0].Count() == 3) return HandRank.ThreeOfAKind;
            
            // Two Pair
            if (groups[0].Count() == 2 && groups[1].Count() == 2) return HandRank.TwoPair;
            
            // Pair
            if (groups[0].Count() == 2) return HandRank.Pair;
            
            return HandRank.HighCard;
        }
        
        private static bool IsStraight(List<int> ranks)
        {
            // Handle Ace-low straight (A,2,3,4,5)
            if (ranks[0] == 2 && ranks[4] == 14)
            {
                return ranks[1] == 3 && ranks[2] == 4 && ranks[3] == 5;
            }
            
            // Regular straight
            for (int i = 1; i < ranks.Count; i++)
            {
                if (ranks[i] != ranks[i-1] + 1) return false;
            }
            return true;
        }
    }
} 