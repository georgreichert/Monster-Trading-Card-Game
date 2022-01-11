using MTCG.Cards;
using MTCG.Cards.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    class TradingParsed
    {
        public string Id { get; set; }
        public string CardToTrade { get; set; }
        public ElementType EType { get; set; }
        public MonsterType MType { get; set; }
        public int MinimumDamage { get; set; }

        public static TradingParsed FromTrading(Trading trading)
        {
            return new()
            {
                Id = trading.Id,
                CardToTrade = trading.CardToTrade,
                EType = TypeMapper.MapElementType(trading.ElementType),
                MType = TypeMapper.MapMonsterType(trading.MonsterType),
                MinimumDamage = trading.MinimumDamage,
            };
        }

        public Trading ToTrading()
        {
            return new Trading()
            {
                Id = Id,
                CardToTrade = CardToTrade,
                ElementType = TypeMapper.MapElementType(EType),
                MonsterType = TypeMapper.MapMonsterType(MType),
                MinimumDamage = MinimumDamage,
            };
        }
    }
}
