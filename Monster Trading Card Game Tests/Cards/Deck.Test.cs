using MTCG.Cards;
using MTCG.Cards.Spells;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Tests.Cards
{
    class DeckTests
    {
        Deck _deck;

        [SetUp]
        public void Setup ()
        {
            _deck = new Deck("Testdeck");
        }

        [Test]
        public void AddCardCountTest()
        {
            _deck.AddCard(new Spell("1", "Bla", ElementType.Normal, 50));
            Assert.AreEqual(1, _deck.Count);
        }

        [Test]
        public void DrawCardCountTest()
        {
            _deck.AddCard(new Spell("1", "Bla", ElementType.Normal, 50));
            _deck.AddCard(new Spell("1", "Bla", ElementType.Normal, 50));
            _deck.AddCard(new Spell("1", "Bla", ElementType.Normal, 50));
            _deck.DrawCard();
            Assert.AreEqual(2, _deck.Count);
        }
    }
}
