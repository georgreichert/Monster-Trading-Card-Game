using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards
{
    abstract class Card
    {
        public string Name { get; }
        public ElementType Type { get; }
        public int Damage { get; }
        public List<Rule> Rules { get; }

        public Card (string name, ElementType type, int damage)
        {
            Name = name;
            Type = type;
            Damage = damage;
            Rules = new List<Rule>();
        }
    }
}
