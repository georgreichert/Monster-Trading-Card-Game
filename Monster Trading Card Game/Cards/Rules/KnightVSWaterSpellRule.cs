using MTCG.Cards.Monsters;
using MTCG.Cards.Spells;
using MTCG.Game;
using System;

namespace MTCG.Cards.Rules
{
    internal class KnightVSWaterSpellRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            Monster ruledMonster = ruledCard as Monster;
            if (ruledMonster == null || ruledMonster.MType != MonsterType.Knight)
            {
                throw new ArgumentException("ruledCard must be a Monster of type Knight");
            }

            Spell otherSpell = otherCard as Spell;
            if (otherSpell == null || otherSpell.EType != ElementType.Water)
            {
                return;
            }

            ruledCard.Destroy();
            game.AddBattleLog($"{ruledCard.Name}'s armor is too heavy, {otherCard.Name} is drowning him!");
        }
    }
}