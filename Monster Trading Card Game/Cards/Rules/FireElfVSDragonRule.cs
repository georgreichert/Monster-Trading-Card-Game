using MTCG.Cards.Monsters;
using MTCG.Game;
using System;

namespace MTCG.Cards.Rules
{
    internal class FireElfVSDragonRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            Monster ruledMonster = ruledCard as Monster;
            if (ruledMonster == null || ruledMonster.MType != MonsterType.Elf || ruledCard.EType != ElementType.Fire)
            {
                throw new ArgumentException("ruledCard must be a Monster of type Elf with element type Fire");
            }

            Monster otherMonster = otherCard as Monster;
            if (otherMonster == null || otherMonster.MType != MonsterType.Dragon)
            {
                return;
            }

            otherCard.NoDamage();
            game.AddBattleLog($"{ruledCard.Name} evades {otherCard.Name}'s attacks with ease!");
        }
    }
}