using MTCG.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    class CardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }

        public static CardModel FromCard(Card card)
        {
            return new CardModel()
            {
                Id = card.ID,
                Name = card.Name,
                Damage = card.Damage
            };
        }
    }
}
