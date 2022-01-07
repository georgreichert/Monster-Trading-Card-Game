using MTCG.Cards;
using MTCG.Cards.Monsters;
using MTCG.Cards.Rules;
using MTCG.Cards.Spells;
using MTCG.Game;
using NUnit.Framework;
using System;

namespace MTCG.Tests.Cards.Rules
{
    public class FireElfVSDragonRuleTests
    {
        GameController _game;
        Rule _testRule;

        [SetUp]
        public void Setup()
        {
            _game = new GameController(new Deck("Test1"), new Deck("Test2"));
            _testRule = new FireElfVSDragonRule();
        }

        [Test]
        public void RuledCardWrongMonsterTypeTest()
        {
            Card ruledCard = new Monster("1", "Goblin", ElementType.Fire, 40, MonsterType.Goblin);
            Card otherCard = new Spell("1", "Test", ElementType.Fire, 50);
            Assert.Throws<ArgumentException>(() => _testRule.Apply(ruledCard, otherCard, _game));
        }

        [Test]
        public void RuledCardWrongCardTypeTest()
        {
            Card ruledCard = new Spell("1", "Goblin", ElementType.Fire, 40);
            Card otherCard = new Spell("1", "Test", ElementType.Fire, 50);
            Assert.Throws<ArgumentException>(() => _testRule.Apply(ruledCard, otherCard, _game));
        }

        [Test]
        public void FireElfVSDragonTest()
        {
            Card ruledCard = new Monster("1", "FireElf", ElementType.Fire, 40, MonsterType.Elf);
            Card otherCard = new Monster("1", "Dragon", ElementType.Fire, 50, MonsterType.Dragon);
            _testRule.Apply(ruledCard, otherCard, _game);
            Assert.That(otherCard.OffensiveDamage == 0);
        }

        [Test]
        public void FireElfVSGoblinTest()
        {
            Card ruledCard = new Monster("1", "FireElf", ElementType.Fire, 40, MonsterType.Elf);
            Card otherCard = new Monster("1", "Goblin", ElementType.Fire, 50, MonsterType.Goblin);
            _testRule.Apply(ruledCard, otherCard, _game);
            Assert.That(otherCard.OffensiveDamage == 50);
        }
    }
}