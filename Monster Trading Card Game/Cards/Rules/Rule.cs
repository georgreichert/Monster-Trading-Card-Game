using MTCG.Game;

namespace MTCG.Cards.Rules
{
    abstract class Rule
    {
        public abstract void Apply(Card ruledCard, Card otherCard, GameController game);
    }
}