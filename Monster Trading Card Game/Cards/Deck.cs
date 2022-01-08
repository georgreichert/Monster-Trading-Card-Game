using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Cards
{
    public class Deck
    {
        private List<Card> _cards = new List<Card>();
        public string Name { get; set; }
        public int Count { get; private set; } = 0;

        public Deck(string name)
        {
            Name = name;
        }

        public void AddCard(Card card)
        {
            Count++;
            _cards.Add(card);
        }

        public Card DrawCard()
        {
            Random rand = new Random();
            int randIndex = rand.Next(_cards.Count);
            Card r = _cards[randIndex];
            _cards.RemoveAt(randIndex);
            Count--;
            return r;
        }

        public IEnumerable<Card> GetCards()
        {
            return _cards;
        }
    }
}
