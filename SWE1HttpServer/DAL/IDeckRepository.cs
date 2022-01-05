using MTCG.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    interface IDeckRepository
    {
        public Deck GetDeck(string username);
        public void AddDeck(Deck deck, string username);
        public void RemoveDeck(string username);
        public void AddCardToDeck(string cardID, string username);
    }
}
