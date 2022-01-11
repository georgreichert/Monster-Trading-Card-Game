using MTCG.Cards;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    class InMemoryCardRepository : ICardRepository
    {
        private readonly Dictionary<string, Tuple<string, Card>> _cards = new();
        private readonly Dictionary<string, Deck> _decks = new();
        private readonly List<List<Card>> _packages = new();
        private readonly Random _random = new Random();
        private readonly Dictionary<string, TradingParsed> _tradings = new();

        public void BundlePackage(string[] ids)
        {
            if (ids.Length != 5)
            {
                throw new ArgumentException("Packages must have exactly 5 Cards in them!");
            }
            List<Card> package = new();
            foreach (string id in ids)
            {
                if (_cards[id].Item1 == "")
                {
                    package.Add(_cards[id].Item2);
                } else
                {
                    throw new ArgumentException($"Card {id} is already owned by {_cards[id].Item1}!");
                }
            }
            _packages.Add(package);
        }

        public void AssignCardToDeck(string id)
        {
            Tuple<string, Card> card = _cards[id];
            if (card.Item1 != "")
            {
                int count;
                try
                {
                    count = _decks[card.Item1].Count;
                }
                catch (KeyNotFoundException)
                {
                    _decks.Add(card.Item1, new Deck($"{card.Item1}'s Deck"));
                    count = 0;
                }
                if (count <= 3)
                {
                    _decks[card.Item1].AddCard(card.Item2);
                }
                else
                {
                    throw new DeckException("Can't add more than 4 cards to a deck!");
                }
            } else
            {
                throw new UnauthorizedAccessException($"The card with ID {id} is not owned by any user.");
            }
        }

        public void AssignCardToUser(string id, string user)
        {
            _cards[id] = new Tuple<string, Card>(user, _cards[id].Item2);
        }

        public void DeleteCard(string id)
        {
            if (!_cards.Remove(id)) throw new KeyNotFoundException();
        }

        public Card GetCard(string id)
        {
            return _cards[id].Item2;
        }

        public IEnumerable<Card> GetCards(string username)
        {
            List<Card> r = new();
            foreach (KeyValuePair<string, Tuple<string, Card>> card in _cards)
            {
                if (card.Value.Item1 == username)
                {
                    r.Add(card.Value.Item2);
                }
            }
            return r;
        }

        public Deck GetDeck(string user)
        {
            try
            {
                return _decks[user];
            } catch (KeyNotFoundException)
            {
                return new Deck($"{user}'s Deck");
            }
        }

        public void GiveRandomPackageToUser(string user)
        {
            if (_packages.Count == 0)
            {
                throw new NoPackagesException("There are no more Packages on the server.");
            }
            int rand = _random.Next(0, _packages.Count - 1);
            List<Card> package = _packages[rand];
            _packages.RemoveAt(rand);
            foreach (Card card in package)
            {
                _cards[card.ID] = new Tuple<string, Card>(user, card);
            }
        }

        public void InsertCard(Card card)
        {
            _cards.Add(card.ID, new Tuple<string, Card>("", card));
        }

        public void RemoveCardFromDeck(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDeck(string user)
        {
            if (!_decks.Remove(user))
            {
                throw new KeyNotFoundException($"Could not delete deck for user {user}");
            }
        }

        public bool IsOwner(string[] ids, string user)
        {
            bool r = true;
            IEnumerable<Card> cards = GetCards(user);
            foreach (string id in ids)
            {
                if (!(_cards.ContainsKey(id) && _cards[id].Item1 == user))
                {
                    r = false;
                    break;
                }
            }
            return r;
        }

        public Trading[] GetTradings()
        {
            Trading[] r = new Trading[_tradings.Count];
            int i = 0;
            foreach (var parsed in _tradings)
            {
                r[i] = parsed.Value.ToTrading();
            }
            return r;
        }

        public void AddTrading(TradingParsed trading)
        {
            if (_tradings.ContainsKey(trading.Id))
            {
                throw new DuplicateTradingException($"A trading with ID {trading.Id} already exists!");
            }
            _tradings.Add(trading.Id, trading);
        }

        public TradingParsed GetTrading(string id)
        {
            return _tradings[id];
        }

        public void DeleteTrading(string id)
        {
            _tradings.Remove(id);
        }

        public void Trade(string id, string cardToTrade)
        {
            Card card1 = _cards[cardToTrade].Item2;
            Card card2 = _cards[GetTrading(id).CardToTrade].Item2;
            string temp = _cards[cardToTrade].Item1;
            string cardToGet = GetTrading(id).CardToTrade;
            _cards[cardToTrade] = new(_cards[cardToGet].Item1, card2);
            _cards[cardToGet] = new(temp, card1);
        }
    }
}
