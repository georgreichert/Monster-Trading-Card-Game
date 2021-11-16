using MTCG.Cards.Monsters;
using MTCG.Game;
using System;

namespace MTCG.Cards.Rules
{
    internal class GoblinVSDragonRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            Monster ruledMonster = ruledCard as Monster;
            if (ruledMonster == null || ruledMonster.MType != MonsterType.Goblin)
            {
                throw new ArgumentException("ruledCard must be a Monster of type Goblin");
            }

            Monster otherMonster = otherCard as Monster;
            if (otherMonster == null || otherMonster.MType != MonsterType.Dragon)
            {
                return;
            }

            ruledCard.NoDamage();
            game.AddBattleLog($"{ruledCard.Name} can't attack {otherCard.Name}, he is trembling with fear!");
        }
    }
}