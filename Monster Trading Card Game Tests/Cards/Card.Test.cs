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
    class CardTests
    {
        Card _testCard;

        [SetUp]
        public void SetUp ()
        {
            _testCard = new Spell("1", "Test1", ElementType.Normal, 50);
        }

        [Test]
        public void EffectiveOffensiveTest()
        {
            _testCard.Effective();
            Assert.AreEqual(100, _testCard.OffensiveDamage);
        }

        [Test]
        public void EffectiveDefensiveTest()
        {
            _testCard.Effective();
            Assert.AreEqual(100, _testCard.DefensiveDamage);
        }

        [Test]
        public void NotEffectiveOffensiveTest()
        {
            _testCard.NotEffective();
            Assert.AreEqual(25, _testCard.OffensiveDamage);
        }

        [Test]
        public void NotEffectiveDefensiveTest()
        {
            _testCard.NotEffective();
            Assert.AreEqual(25, _testCard.DefensiveDamage);
        }

        [Test]
        public void NoDamageOffensiveTest()
        {
            _testCard.NoDamage();
            Assert.AreEqual(0, _testCard.OffensiveDamage);
        }

        [Test]
        public void NoDamageDefensiveTest()
        {
            _testCard.NoDamage();
            Assert.AreEqual(50, _testCard.DefensiveDamage);
        }

        [Test]
        public void DestroyTest()
        {
            _testCard.Destroy();
            Assert.That(_testCard.Destroyed);
        }

        [Test]
        public void ResetOffensiveTest()
        {
            _testCard.Effective();
            _testCard.Reset();
            Assert.AreEqual(50, _testCard.OffensiveDamage);
        }

        [Test]
        public void ResetDefensiveTest()
        {
            _testCard.Effective();
            _testCard.Reset();
            Assert.AreEqual(50, _testCard.DefensiveDamage);
        }
    }
}
