using MTCG.Cards.Monsters;
using MTCG.Game;
using System;

namespace MTCG.Cards.Rules
{
    public class WizardVSOrcRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            Monster ruledMonster = ruledCard as Monster;
            if (ruledMonster == null || ruledMonster.MType != MonsterType.Wizard)
            {
                throw new ArgumentException("ruledCard must be a Monster of type Wizard");
            }

            Monster otherMonster = otherCard as Monster;
            if (otherMonster == null || otherMonster.MType != MonsterType.Orc)
            {
                return;
            }

            otherCard.NoDamage();
            game.AddBattleLog($"{ruledCard.Name} controls {otherCard.Name}'s mind, he can't attack!");
        }
    }
}