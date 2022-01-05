using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards.Spells
{
    public class Spell : Card
    {
        public Spell(string id, string name, ElementType type, int damage) : base(id, name, type, damage)
        {
        }
    }
}
