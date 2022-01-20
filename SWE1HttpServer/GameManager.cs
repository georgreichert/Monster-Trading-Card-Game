using MTCG.Cards;
using MTCG.Cards.Monsters;
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
            var user = _userRepository.LoginUser(credentials.Username, credentials.Password);
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

        public void GivePackageToUser(string username)
        {
            _cardRepository.GivePackageToUser(username);
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
            foreach (string id in ids)
            {
                if (_cardRepository.IsCardInTrading(id))
                {
                    throw new CardBlockedException($"Card with ID {id} is offered in a trading and cannot be assigned to a deck.");
                }
                if (_cardRepository.IsCardInSale(id))
                {
                    throw new CardBlockedException($"Card with ID {id} is offered in a sale and cannot be assigned to a deck.");
                }
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

        public ScoreboardEntry[] GetScoreBoard()
        {
            return _userRepository.GetScoreBoard();
        }

        public List<string> Battle(string username)
        {
            if (_battleManager.IsEnqueued(username))
            {
                throw new DuplicateBattleEnqueueException($"User {username} is already enqueued for battle.");
            }
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

        public Trading[] GetTradings()
        {
            return _cardRepository.GetTradings();
        }

        public void AddTrading(Trading trading, string username)
        {
            if (trading.Id == "" || trading.CardToTrade == "")
            {
                throw new ArgumentException("One ore more arguments missing.");
            }
            if (!_cardRepository.IsOwner(new string[1] { trading.CardToTrade }, username)) {
                throw new UnauthorizedAccessException($"The card with ID {trading.CardToTrade} does not belong to user {username}");
            }
            if (_cardRepository.IsCardInDeck(trading.CardToTrade))
            {
                throw new CardBlockedException($"The card with ID {trading.CardToTrade} is in a deck and cannot be traded.");
            }
            if (_cardRepository.IsCardInTrading(trading.CardToTrade))
            {
                throw new CardBlockedException($"The card with ID {trading.CardToTrade} is already offered for trade.");
            }
            _cardRepository.AddTrading(TradingParsed.FromTrading(trading));
        }

        public void DeleteTrading(string id, string username)
        {
            var trading = _cardRepository.GetTrading(id);
            if (!_cardRepository.IsOwner(new string[1] { trading.CardToTrade }, username))
            {
                throw new UnauthorizedAccessException($"The trading with ID {id} is not deletable by user {username}");
            }
            _cardRepository.DeleteTrading(id);
        }

        public void Trade(string id, string cardToTrade, string username)
        {
            var card = _cardRepository.GetCard(cardToTrade);
            TradingParsed trading = _cardRepository.GetTrading(id);
            if (_cardRepository.IsOwner(new string[1] { trading.CardToTrade }, username))
            {
                throw new ArgumentException("You can't trade with yourself.");
            }
            if (!_cardRepository.IsOwner(new string[1] { cardToTrade }, username))
            {
                throw new ArgumentException("You can't trade cards that don't belong to you.");
            }
            _cardRepository.Trade(id, cardToTrade);
        }

        public Card GetCard(string id)
        {
            return _cardRepository.GetCard(id);
        }

        public void AddSale(Sale sale, string username)
        {
            _cardRepository.AddSale(sale, username);
        }

        public void DeleteSale(string id, string username)
        {
            Sale sale = _cardRepository.GetSale(id);
            if (!_cardRepository.IsOwner(new string[1] { sale.CardToSell }, username))
            {
                throw new UnauthorizedAccessException($"The sale with ID {id} is not deletable by user {username}");
            }
            _cardRepository.DeleteSale(id);
        }

        public void Buy(string id, string username)
        {
            Sale sale = _cardRepository.GetSale(id);
            if (_cardRepository.IsOwner(new string[1] { sale.CardToSell }, username))
            {
                throw new ArgumentException("You can't buy your own cards.");
            }

            _cardRepository.Buy(id, username);
        }

        public Sale[] GetSales()
        {
            return _cardRepository.GetSales();
        }
    }
}
