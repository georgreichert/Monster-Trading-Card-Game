using MTCG.Cards;
using Npgsql;
using Server.Models;
using System.Collections.Generic;

namespace Server.DAL
{
    public class DatabaseRepository : IUserRepository, ICardRepository
    {
        private NpgsqlConnection _connection;

        public DatabaseRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public void AddTrading(TradingParsed trading)
        {
            throw new System.NotImplementedException();
        }

        public void AlterStats(string username, BattleResult result)
        {
            throw new System.NotImplementedException();
        }

        public void AssignCardToDeck(string id)
        {
            throw new System.NotImplementedException();
        }

        public void AssignCardToUser(string id, string user)
        {
            throw new System.NotImplementedException();
        }

        public void BundlePackage(string[] ids)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCard(string id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTrading(string id)
        {
            throw new System.NotImplementedException();
        }

        public Card GetCard(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Card> GetCards(string username)
        {
            throw new System.NotImplementedException();
        }

        public Deck GetDeck(string user)
        {
            throw new System.NotImplementedException();
        }

        public ScoreboardEntry[] GetScoreBoard()
        {
            throw new System.NotImplementedException();
        }

        public TradingParsed GetTrading(string id)
        {
            throw new System.NotImplementedException();
        }

        public Trading[] GetTradings()
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByAuthToken(string authToken)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByCredentials(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public UserPublicData GetUserPublicData(string username)
        {
            throw new System.NotImplementedException();
        }

        public Stats GetUserStats(string username)
        {
            throw new System.NotImplementedException();
        }

        public void GivePackageToUser(string user)
        {
            throw new System.NotImplementedException();
        }

        public void InsertCard(Card card)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool IsOwner(string[] ids, string user)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCardFromDeck(string id)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDeck(string user)
        {
            throw new System.NotImplementedException();
        }

        public void SetUserPublicData(string username, UserPublicData data)
        {
            throw new System.NotImplementedException();
        }

        public void Trade(string id, string cardToTrade)
        {
            throw new System.NotImplementedException();
        }
    }
}