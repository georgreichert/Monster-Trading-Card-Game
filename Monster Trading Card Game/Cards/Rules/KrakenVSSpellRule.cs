using MTCG.Cards.Monsters;
using MTCG.Cards.Spells;
using MTCG.Game;
using System;

namespace MTCG.Cards.Rules
{
    internal class KrakenVSSpellRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            Monster ruledMonster = ruledCard as Monster;
            if (ruledMonster == null || ruledMonster.MType != MonsterType.Kraken)
            {
                throw new ArgumentException("ruledCard must be a Monster of type Kraken");
            }
            
            Spell otherSpell = otherCard as Spell;
            if (otherSpell == null)
            {
                return;
            }

            otherCard.NoDamage();
            game.AddBattleLog($"{ruledCard.Name}, can't be harmed by {otherCard.Name}'s magic might.");
        }
    }
}