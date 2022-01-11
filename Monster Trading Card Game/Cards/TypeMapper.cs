using MTCG.Cards.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards
{
    public class TypeMapper
    {
        private readonly static Dictionary<ElementType, string> _elementTypeMap = new()
        {
            { ElementType.Fire, "Fire" },
            { ElementType.Water, "Water" },
            { ElementType.Normal, "Normal" },
            { ElementType.Any, "Any" },
            { ElementType.None, "None" },
        };

        private readonly static Dictionary<MonsterType, string> _monsterTypeMap = new()
        {
            { MonsterType.Dragon, "Dragon" },
            { MonsterType.Elf, "Elf" },
            { MonsterType.Goblin, "Goblin" },
            { MonsterType.Knight, "Knight" },
            { MonsterType.Kraken, "Kraken" },
            { MonsterType.Orc, "Orc" },
            { MonsterType.Other, "Other" },
            { MonsterType.Wizard, "Wizard" },
            { MonsterType.Any, "Any" },
            { MonsterType.None, "None" },
        };

        public static MonsterType MapMonsterType(string type)
        {
            var r = _monsterTypeMap.SingleOrDefault((a) => a.Value == type);
            if ((r.Key, r.Value) == default)
            {
                throw new KeyNotFoundException($"Could not convert '{type}' to a MonsterType.");
            }
            return r.Key;
        }

        public static string MapMonsterType(MonsterType type)
        {
            return _monsterTypeMap[type];
        }

        public static ElementType MapElementType(string type)
        {
            var r = _elementTypeMap.SingleOrDefault((a) => a.Value == type);
            if ((r.Key, r.Value) == default)
            {
                throw new KeyNotFoundException($"Could not convert '{type}' to an ElementType.");
            }
            return r.Key;
        }

        public static string MapElementType(ElementType type)
        {
            return _elementTypeMap[type];
        }
    }
}
