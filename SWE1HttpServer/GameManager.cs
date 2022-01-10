using MTCG.Cards;
using MTCG.Cards.Spells;
using Server.DAL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class GameManager : IGameManager
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBattleManager _battleManager;

        public GameManager(ICardRepository cardRepository, IUserRepository userRepository, IBattleManager battleManager)
        {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _battleManager = battleManager;
            _battleManager.Start();
        }

        public User LoginUser(Credentials credentials)
        {
            var user = _userRepository.GetUserByCredentials(credentials.Username, credentials.Password);
            return user ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User(0, 0, 0, 100)
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

        public void ConfigureDeck(string[] ids, string username)
        {
            if(!_cardRepository.IsOwner(ids, username))
            {
                throw new UnauthorizedAccessException($"One or more of the chosen cards do not belong to the user {username}");
            }
            if (ids.Length != 4)
            {
                throw new ArgumentException("Exactly 4 IDs expected.");
            }
            try
            {
                _cardRepository.RemoveDeck(username);
            }
            catch (KeyNotFoundException)
            {
            }
            foreach (string id in ids)
            {
                _cardRepository.AssignCardToDeck(id);
            }
        }

        public UserPublicData GetUserPublicData(string username)
        {
            return _userRepository.GetUserPublicData(username);
        }

        public void SetUserPublicData(string username, UserPublicData data)
        {
            _userRepository.SetUserPublicData(username, data);
        }

        public Stats GetStats(string username)
        {
            return _userRepository.GetUserStats(username);
        }

        public ScoreBoard GetScoreBoard()
        {
            return _userRepository.GetScoreBoard();
        }

        public List<string> Battle(string username)
        {
            Deck deck = _cardRepository.GetDeck(username);
            if (deck.Count != 4)
            {
                throw new DeckException("Your deck must contain exactly 4 cards to enqueue for battle.");
            }
            _battleManager.EnterQueue(username, deck);
            Thread.Sleep(200);
            var log = _battleManager.GetBattleLog(username);
            if (log == null)
            {
                throw new ApplicationException("Something went wrong when trying to receive BattleLog, BattleManager was probably stopped.");
            }
            _userRepository.AlterStats(username, log.Item1);
            return log.Item2;
        }
    }
}
