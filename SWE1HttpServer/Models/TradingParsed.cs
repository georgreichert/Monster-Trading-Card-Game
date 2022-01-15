using MTCG.Cards;
using MTCG.Cards.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class TradingParsed
    {
        public string Id { get; set; }
        public string CardToTrade { get; set; }
        public MonsterType Type { get; set; }
        public int MinimumDamage { get; set; }

        public static TradingParsed FromTrading(Trading trading)
        {
            return new()
            {
                Id = trading.Id,
                CardToTrade = trading.CardToTrade,
                Type = trading.Type == "monster" ? MonsterType.Any : 
                    (trading.Type == "spell" ? MonsterType.None : 
                    throw new ArgumentException("Type must be 'monster' or 'spell'")),
                MinimumDamage = trading.MinimumDamage,
            };
        }

        public Trading ToTrading()
        {
            return new Trading()
            {
                Id = Id,
                CardToTrade = CardToTrade,
                Type = Type == MonsterType.Any ? "monster" : "spell",
                MinimumDamage = MinimumDamage,
            };
        }
    }
}
