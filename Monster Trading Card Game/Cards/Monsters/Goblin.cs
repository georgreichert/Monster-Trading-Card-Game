using MTCG.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards.Monsters
{
    class Goblin : Monster
    {
        class GoblinRule : Rule
        {
            public override void Apply(Card ruledCard, Card otherCard, GameController game)
            {
                throw new NotImplementedException();
            }
        }

        public Goblin(string name, ElementType type, int damage) : base(name, type, damage)
        {
            this.Rules.Add(new );
        }
    }
}
