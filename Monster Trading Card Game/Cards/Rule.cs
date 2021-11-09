using MTCG.Game;

namespace MTCG.Cards
{
    abstract class Rule
    {
        public abstract void Apply(Card ruledCard, Card otherCard, GameController game);
    }
}