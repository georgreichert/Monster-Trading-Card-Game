using MTCG.Cards.Spells;
using MTCG.Game;

namespace MTCG.Cards.Rules
{
    public class ElementRule : Rule
    {
        public override void Apply(Card ruledCard, Card otherCard, GameController game)
        {
            if (ruledCard as Spell != null || otherCard as Spell != null)
            {
                if ((ruledCard.EType == ElementType.Fire && otherCard.EType == ElementType.Water)
                    || (ruledCard.EType == ElementType.Water && otherCard.EType == ElementType.Normal)
                    || (ruledCard.EType == ElementType.Normal && otherCard.EType == ElementType.Fire))
                {
                    ruledCard.NotEffective();
                    game.AddBattleLog($"{ruledCard.Name} is not effective!");
                } else if (ruledCard.EType == ElementType.Fire && otherCard.EType == ElementType.Normal
                    || (ruledCard.EType == ElementType.Water && otherCard.EType == ElementType.Fire)
                    || (ruledCard.EType == ElementType.Normal && otherCard.EType == ElementType.Water))
                {
                    ruledCard.Effective();
                    game.AddBattleLog($"{ruledCard.Name} is very effective!");
                }
            }
        }
    }
}