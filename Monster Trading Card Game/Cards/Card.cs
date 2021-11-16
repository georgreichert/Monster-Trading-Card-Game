using MTCG.Cards.Monsters;
using MTCG.Cards.Rules;
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
        public ElementType EType { get; }
        public float Damage { get; }
        public bool Destroyed { get; private set; } = false;
        public float OffensiveDamage
        {
            get
            {
                float value = Damage;
                foreach (float factor in _offensiveDamageFactors)
                {
                    value *= factor;
                }
                return value;
            }
        }
        public float DefensiveDamage
        {
            get
            {
                float value = Damage;
                foreach (float factor in _defensiveDamageFactors)
                {
                    value *= factor;
                }
                return value;
            }
        }
        public List<Rule> Rules { get; }
        private List<float> _offensiveDamageFactors = new List<float>();
        private List<float> _defensiveDamageFactors = new List<float>();

        public Card (string name, ElementType type, int damage)
        {
            Name = name;
            EType = type;
            Damage = damage;
            Rules = new List<Rule>();
            Rules.Add(new ElementRule());
        }

        internal void Destroy()
        {
            Destroyed = true;
        }

        internal void NoDamage()
        {
            _offensiveDamageFactors.Add(0f);
        }

        internal void Effective()
        {
            _offensiveDamageFactors.Add(2f);
            _defensiveDamageFactors.Add(2f);
        }

        internal void NotEffective()
        {
            _offensiveDamageFactors.Add(0.5f);
            _defensiveDamageFactors.Add(0.5f);
        }

        internal void Reset()
        {
            _offensiveDamageFactors.Clear();
            _defensiveDamageFactors.Clear();
            Destroyed = false;
        }
    }
}
