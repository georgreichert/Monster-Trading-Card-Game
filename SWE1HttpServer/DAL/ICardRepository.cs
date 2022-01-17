using MTCG.Cards;
using Server.Models;
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
        public void AssignCardToUser(string id, string user);
        public void AssignCardToDeck(string id);
        public void RemoveCardFromDeck(string id);
        public Deck GetDeck(string user);
        public void RemoveDeck(string user);
        public void BundlePackage(string[] ids);
        public void GivePackageToUser(string user);
        public bool IsOwner(string[] ids, string user);
        public Trading[] GetTradings();
        void AddTrading(TradingParsed trading);
        TradingParsed GetTrading(string id);
        void DeleteTrading(string id);
        void Trade(string tradingId, string cardToTrade);
        bool IsCardInTrading(string id);
        bool IsCardInDeck(string id);
    }
}
