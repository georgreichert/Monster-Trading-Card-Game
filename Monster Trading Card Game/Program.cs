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

            deck1.AddCard(new Monster("1", "Useless Knight", ElementType.Normal, 40, MonsterType.Knight));
            deck1.AddCard(new Monster("2", "Fearless Fire Elf", ElementType.Fire, 70, MonsterType.Elf));
            deck1.AddCard(new Spell("3", "Firebolt", ElementType.Fire, 50));
            deck1.AddCard(new Spell("4", "Squirt", ElementType.Water, 40));

            deck2.AddCard(new Monster("5", "One-Winged Dragon", ElementType.Fire, 40, MonsterType.Dragon));
            deck2.AddCard(new Monster("6", "Mighty Kraken", ElementType.Water, 70, MonsterType.Kraken));
            deck2.AddCard(new Spell("7", "Flood", ElementType.Water, 50));
            deck2.AddCard(new Spell("8", "Stomp", ElementType.Normal, 40));

            GameController game = new GameController(deck1, deck2);
            game.Play();
            game.PrintLog();
        }
    }
}
