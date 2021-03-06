using MTCG.Cards.Monsters;
using MTCG.Cards.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards
{
    public abstract class Card
    {
        public string ID { get; }
        public string Name { get; }
        public ElementType EType { get; }
        public float Damage { get; }
        public bool Destroyed { get; private set; } = false;
        public bool Guarded { get; private set; }
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

        public Card(string id, string name, ElementType type, int damage)
        {
            ID = id;
            Name = name;
            EType = type;
            Damage = damage;
            Rules = new List<Rule>();
            Rules.Add(new ElementRule());
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public void NoDamage()
        {
            _offensiveDamageFactors.Add(0f);
        }

        public void Effective()
        {
            _offensiveDamageFactors.Add(2f);
            _defensiveDamageFactors.Add(2f);
        }

        public void NotEffective()
        {
            _offensiveDamageFactors.Add(0.5f);
            _defensiveDamageFactors.Add(0.5f);
        }

        public void Reset()
        {
            _offensiveDamageFactors.Clear();
            _defensiveDamageFactors.Clear();
            Destroyed = false;
        }

        internal void Guard()
        {
            Guarded = true;
        }

        public bool UseGuard()
        {
            bool guard = Guarded;
            Guarded = false;
            return guard;
        }
    }
}
