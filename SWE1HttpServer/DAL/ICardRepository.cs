using MTCG.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    interface ICardRepository
    {
        public IEnumerable<Card> GetCards(string username);
        public Card GetCard(string id);
        public void InsertCard(Card card);
        public void DeleteCard(string id);
    }
}
