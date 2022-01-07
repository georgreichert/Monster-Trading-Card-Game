using Moq;
using MTCG.Cards;
using MTCG.Cards.Monsters;
using MTCG.Cards.Rules;
using MTCG.Cards.Spells;
using MTCG.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Tests.Cards.Rules
{
    class ElementRuleTests
    {
        Rule _rule;
        GameController _game;

        [SetUp]
        public void Setup ()
        {
            _rule = new ElementRule();
            _game = new GameController(new Deck("Test1"), new Deck("Test2"));
        }

        [Test]
        [TestCase(ElementType.Fire, ElementType.Water, 25)]
        [TestCase(ElementType.Fire, ElementType.Normal, 100)]
        [TestCase(ElementType.Fire, ElementType.Fire, 50)]
        [TestCase(ElementType.Water, ElementType.Water, 50)]
        [TestCase(ElementType.Water, ElementType.Normal, 25)]
        [TestCase(ElementType.Water, ElementType.Fire, 100)]
        [TestCase(ElementType.Normal, ElementType.Water, 100)]
        [TestCase(ElementType.Normal, ElementType.Normal, 50)]
        [TestCase(ElementType.Normal, ElementType.Fire, 25)]
        public void SpellVSSpellTest (ElementType ruledCardEType, ElementType otherCardEType, float expected)
        {
            Spell ruledCard = new Spell("1", "Test1", ruledCardEType, 50);
            Spell otherCard = new Spell("1", "Test2", otherCardEType, 50);
            _rule.Apply(ruledCard, otherCard, _game);
            Assert.AreEqual(expected, ruledCard.OffensiveDamage);
        }

        [Test]
        [TestCase(ElementType.Fire, ElementType.Water, 25)]
        [TestCase(ElementType.Fire, ElementType.Normal, 100)]
        [TestCase(ElementType.Fire, ElementType.Fire, 50)]
        [TestCase(ElementType.Water, ElementType.Water, 50)]
        [TestCase(ElementType.Water, ElementType.Normal, 25)]
        [TestCase(ElementType.Water, ElementType.Fire, 100)]
        [TestCase(ElementType.Normal, ElementType.Water, 100)]
        [TestCase(ElementType.Normal, ElementType.Normal, 50)]
        [TestCase(ElementType.Normal, ElementType.Fire, 25)]
        public void SpellVSMonsterTest(ElementType ruledCardEType, ElementType otherCardEType, float expected)
        {
            Spell ruledCard = new Spell("1", "Test1", ruledCardEType, 50);
            Monster otherCard = new Monster("1", "Test2", otherCardEType, 50, MonsterType.Other);
            _rule.Apply(ruledCard, otherCard, _game);
            Assert.AreEqual(expected, ruledCard.OffensiveDamage);
        }

        [Test]
        [TestCase(ElementType.Fire, ElementType.Water, 50)]
        [TestCase(ElementType.Fire, ElementType.Normal, 50)]
        [TestCase(ElementType.Fire, ElementType.Fire, 50)]
        [TestCase(ElementType.Water, ElementType.Water, 50)]
        [TestCase(ElementType.Water, ElementType.Normal, 50)]
        [TestCase(ElementType.Water, ElementType.Fire, 50)]
        [TestCase(ElementType.Normal, ElementType.Water, 50)]
        [TestCase(ElementType.Normal, ElementType.Normal, 50)]
        [TestCase(ElementType.Normal, ElementType.Fire, 50)]
        public void MonsterVSMonsterTest(ElementType ruledCardEType, ElementType otherCardEType, float expected)
        {
            Monster ruledCard = new Monster("1", "Test1", ruledCardEType, 50, MonsterType.Other);
            Monster otherCard = new Monster("1", "Test2", otherCardEType, 50, MonsterType.Other);
            _rule.Apply(ruledCard, otherCard, _game);
            Assert.AreEqual(expected, ruledCard.OffensiveDamage);
        }
    }
}
