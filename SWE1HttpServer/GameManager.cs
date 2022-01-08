using MTCG.Cards;
using MTCG.Cards.Spells;
using Server.DAL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class GameManager : IGameManager
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserRepository _userRepository;
        public GameManager(ICardRepository cardRepository, IUserRepository userRepository)
        {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
        }

        public User LoginUser(Credentials credentials)
        {
            var user = _userRepository.GetUserByCredentials(credentials.Username, credentials.Password);
            return user ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User()
            {
                Username = credentials.Username,
                Password = credentials.Password
            };
            if (_userRepository.InsertUser(user) == false)
            {
                throw new DuplicateUserException();
            }
        }

        public void AddCard(Card card)
        {
            _cardRepository.InsertCard(card);
        }

        public void BundlePackage(string[] ids)
        {
            _cardRepository.BundlePackage(ids);
        }

        public void GiveRandomPackageToUser(string username)
        {
            _cardRepository.GiveRandomPackageToUser(username);
        }

        public IEnumerable<Card> GetCards(string username)
        {
            return _cardRepository.GetCards(username);
        }

        public Deck GetDeck(string username)
        {
            return _cardRepository.GetDeck(username);
        }
    }
}
