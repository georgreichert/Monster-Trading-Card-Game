using MTCG.Cards.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards.Monsters
{
    public class Monster : Card
    {
        public MonsterType MType { get; }

        public Monster(string id, string name, ElementType eType, int damage, MonsterType mType) : base(id, name, eType, damage)
        {
            MType = mType;
            switch (mType)
            {
                case MonsterType.Goblin:
                    Rules.Add(new GoblinVSDragonRule());
                    break;
                case MonsterType.Elf:
                    if (eType == ElementType.Fire)
                    {
                        Rules.Add(new FireElfVSDragonRule());
                    }
                    break;
                case MonsterType.Knight:
                    Rules.Add(new KnightVSWaterSpellRule());
                    break;
                case MonsterType.Kraken:
                    Rules.Add(new KrakenVSSpellRule());
                    break;
                case MonsterType.Wizard:
                    Rules.Add(new WizardVSOrcRule());
                    break;
            }
        }
    }
}
