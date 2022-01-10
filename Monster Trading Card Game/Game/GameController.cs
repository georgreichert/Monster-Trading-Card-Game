using MTCG.Cards;
using MTCG.Cards.Rules;
using System;
using System.Collections.Generic;

namespace MTCG.Game
{
    public enum Result
    {
        Player1Win,
        Player2Win,
        Draw
    }

    public class GameController
    {
        public List<string> BattleLog { get; } = new List<string>();
        private Deck _deck1, _deck2;

        public GameController(Deck deck1, Deck deck2)
        {
            _deck1 = deck1;
            _deck2 = deck2;
        }

        public Result Play()
        {
            int turnCounter = 1;
            while (_deck1.Count > 0 && _deck2.Count > 0 && turnCounter <= 100)
            {
                AddBattleLog("#############################################\n" +
                            $"################## TURN {turnCounter} {(turnCounter >= 10 ? "" : "#")}##################\n" +
                            $"### Player1: {_deck1.Count} card{(_deck1.Count == 1 ? ' ' : 's')} ### " +
                            $"Player2: {_deck2.Count} card{(_deck2.Count == 1 ? ' ' : 's')} ###\n" +
                            $"#############################################");

                Card card1 = _deck1.DrawCard();
                AddBattleLog($"Player1 plays {card1.Name} ({card1.Damage})");

                Card card2 = _deck2.DrawCard();
                AddBattleLog($"Player2 plays {card2.Name} ({card2.Damage})\n");
                
                foreach (Rule rule in card1.Rules)
                {
                    rule.Apply(card1, card2, this);
                }

                foreach (Rule rule in card2.Rules)
                {
                    rule.Apply(card2, card1, this);
                }

                float card1VisibleDamage = card1.OffensiveDamage > card1.DefensiveDamage ? card1.OffensiveDamage : card1.DefensiveDamage;
                float card2VisibleDamage = card2.OffensiveDamage > card2.DefensiveDamage ? card2.OffensiveDamage : card2.DefensiveDamage;

                AddBattleLog($"\n{card1.Name}: {card1VisibleDamage}\n" +
                    $"{card2.Name}: {card2VisibleDamage}\n");

                string message;
                switch (HandleFight(card1, card2))
                {
                    case BattleOutcome.WinnerCard1:
                        _deck1.AddCard(card1);
                        _deck1.AddCard(card2);
                        message = $"And the winner is ... { card1.Name} with " +
                            $"{card1VisibleDamage} damage!";
                        break;
                    case BattleOutcome.Draw:
                        _deck1.AddCard(card1);
                        _deck2.AddCard(card2);
                        message = $"It's a draw between { card1.Name} and {card2.Name}.";
                        break;
                    case BattleOutcome.WinnerCard2:
                        _deck2.AddCard(card1);
                        _deck2.AddCard(card2);
                        message = $"And the winner is ... { card2.Name} with " +
                            $"{card2VisibleDamage} damage!";
                        break;
                    default:
                        throw new Exception("Invalid return value, can't determine winner.");
                }
                AddBattleLog(message + "\n\n");
                card1.Reset();
                card2.Reset();

                turnCounter++;
            }
            string finalMessage;
            Result result;
            if (_deck1.Count > 0)
            {
                if (_deck2.Count > 0)
                {
                    finalMessage = "############# DRAW #############";
                    result = Result.Draw;
                } else {
                    finalMessage = "############# PLAYER1 WINS #############";
                    result = Result.Player1Win;
                }
            } else
            {
                finalMessage = "############# PLAYER2 WINS #############";
                result = Result.Player2Win;
            }
            AddBattleLog(finalMessage);
            return result;
        }

        private BattleOutcome HandleFight(Card card1, Card card2)
        {
            if (card1.Destroyed)
            {
                if (card2.Destroyed)
                {
                    return BattleOutcome.Draw;
                }
                return BattleOutcome.WinnerCard2;
            } else if (card2.Destroyed)
            {
                return BattleOutcome.WinnerCard1;
            }
            if (card1.OffensiveDamage > card2.DefensiveDamage)
            {
                return BattleOutcome.WinnerCard1;
            } else if (card2.OffensiveDamage > card1.DefensiveDamage)
            {
                return BattleOutcome.WinnerCard2;
            }
            return BattleOutcome.Draw;
        }
        private enum BattleOutcome
        {
            WinnerCard1,
            WinnerCard2,
            Draw
        }

        internal void AddBattleLog(string log)
        {
            BattleLog.Add(log);
        }

        public void PrintLog()
        {
            foreach (string log in BattleLog)
            {
                Console.WriteLine(log);
            }
        }
    }
}