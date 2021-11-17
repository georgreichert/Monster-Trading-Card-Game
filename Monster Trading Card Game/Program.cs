using MTCG.Cards;
using MTCG.Cards.Monsters;
using MTCG.Cards.Spells;
using MTCG.Game;
using MTCG.Server;
using System;

namespace MTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string line;
            Console.WriteLine("Enter command - ");
            while ((line = Console.ReadLine()) != "exit")
            {
                // TODO: give command to game logic
                Console.WriteLine("Enter command - ");
            }*/

            Deck deck1 = new Deck("Testdeck1");
            Deck deck2 = new Deck("Testdeck2");

            deck1.AddCard(new Monster("Useless Goblin", ElementType.Normal, 40, MonsterType.Goblin));
            deck1.AddCard(new Monster("Mighty Fire Elemental", ElementType.Fire, 70, MonsterType.Other));
            deck1.AddCard(new Spell("Firebolt", ElementType.Fire, 50));
            deck1.AddCard(new Spell("Squirt", ElementType.Water, 40));

            deck2.AddCard(new Monster("One-Winged Dragon", ElementType.Fire, 40, MonsterType.Dragon));
            deck2.AddCard(new Monster("Mighty Kraken", ElementType.Water, 70, MonsterType.Kraken));
            deck2.AddCard(new Spell("Flood", ElementType.Water, 50));
            deck2.AddCard(new Spell("Stomp", ElementType.Normal, 40));

            GameController game = new GameController(deck1, deck2);
            game.Play();
            game.PrintLog();

            MessageManager manager = new MessageManager();
            manager.AddMessage("Hello World!");
            manager.AddMessage("test");
            var messages = manager.ListMessages();
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.ID}: {message.Content}");
            }
        }
    }
}
