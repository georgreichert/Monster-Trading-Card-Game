using MTCG.Cards;
using MTCG.Cards.Rules;
using System;
using System.Collections.Generic;

namespace MTCG.Game
{
    class GameController
    {
        private List<string> _battleLog = new List<string>();
        private Deck _deck1, _deck2;

        public GameController(Deck deck1, Deck deck2)
        {
            _deck1 = deck1;
            _deck2 = deck2;
        }

        public void Play()
        {
            int turnCounter = 1;
            while (_deck1.Count > 0 && _deck2.Count > 0 && turnCounter <= 100)
            {
                AddBattleLog("#######################\n" +
                            $"###### TURN {turnCounter} {(turnCounter >= 10 ? "" : "#")}#######");
                AddBattleLog($"### Player1: {_deck1.Count} cards ### Player2: {_deck2.Count} cards ###");

                Card card1 = _deck1.DrawCard();
                AddBattleLog($"Player1 plays {card1.Name}");

                Card card2 = _deck2.DrawCard();
                AddBattleLog($"Player2 plays {card2.Name}");
                
                foreach (Rule rule in card1.Rules)
                {
                    rule.Apply(card1, card2, this);
                }

                foreach (Rule rule in card2.Rules)
                {
                    rule.Apply(card2, card1, this);
                }

                AddBattleLog($"##### {card1.Name}: {(card1.OffensiveDamage > card1.DefensiveDamage ? card1.OffensiveDamage : card1.DefensiveDamage)}" +
                    $" #### " +
                    $"{card2.Name}: {(card2.OffensiveDamage > card2.DefensiveDamage ? card2.OffensiveDamage : card2.DefensiveDamage)} #####");

                string message;
                switch (HandleFight(card1, card2))
                {
                    case -1:
                        _deck1.AddCard(card1);
                        _deck1.AddCard(card2);
                        message = $"And the winner is ... { card1.Name} with " +
                            $"{(card1.OffensiveDamage > card1.DefensiveDamage ? card1.OffensiveDamage : card1.DefensiveDamage)} damage!";
                        break;
                    case 0:
                        _deck1.AddCard(card1);
                        _deck2.AddCard(card2);
                        message = $"It's a draw between { card1.Name} and {card2.Name}.";
                        break;
                    case 1:
                        _deck2.AddCard(card1);
                        _deck2.AddCard(card2);
                        message = $"And the winner is ... { card2.Name} with " +
                            $"{(card2.OffensiveDamage > card2.DefensiveDamage ? card2.OffensiveDamage : card2.DefensiveDamage)} damage!";
                        break;
                    default:
                        throw new Exception("Invalid return value, can't determine winner.");
                }
                AddBattleLog(message);
                AddBattleLog("");
                card1.Reset();
                card2.Reset();

                turnCounter++;
            }
            string finalMessage;
            if (_deck1.Count > 0)
            {
                if (_deck2.Count > 0)
                {
                    finalMessage = "############# DRAW #############";
                } else {
                    finalMessage = "############# PLAYER1 WINS #############";
                }
            } else
            {
                finalMessage = "############# PLAYER2 WINS #############";
            }
            AddBattleLog(finalMessage);
        }

        private int HandleFight(Card card1, Card card2)
        {
            if (card1.Destroyed)
            {
                if (card2.Destroyed)
                {
                    return 0;
                }
                return 1;
            } else if (card2.Destroyed)
            {
                return -1;
            }
            if (card1.OffensiveDamage > card2.DefensiveDamage)
            {
                return -1;
            } else if (card2.OffensiveDamage > card1.DefensiveDamage)
            {
                return 1;
            }
            return 0;
        }

        internal void AddBattleLog(string log)
        {
            _battleLog.Add(log);
        }

        public void PrintLog()
        {
            foreach (string log in _battleLog)
            {
                Console.WriteLine(log);
            }
        }
    }
}