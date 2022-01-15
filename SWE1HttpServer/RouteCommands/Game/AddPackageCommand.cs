using MTCG.Cards;
using MTCG.Cards.Monsters;
using MTCG.Cards.Spells;
using Server.Core.Response;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class AddPackageCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly CardModel[] _cards;
        private static readonly Dictionary<string, Tuple<MonsterType, ElementType>> _cardMappings = new()
        {
            { "WaterGoblin", new Tuple<MonsterType, ElementType>(MonsterType.Goblin, ElementType.Water) },
            { "Kraken", new Tuple<MonsterType, ElementType>(MonsterType.Kraken, ElementType.Water) },
            { "Dragon", new Tuple<MonsterType, ElementType>(MonsterType.Dragon, ElementType.Fire) },
            { "FireElf", new Tuple<MonsterType, ElementType>(MonsterType.Elf, ElementType.Fire) },
            { "Ork", new Tuple<MonsterType, ElementType>(MonsterType.Orc, ElementType.Normal) },
            { "Knight", new Tuple<MonsterType, ElementType>(MonsterType.Knight, ElementType.Normal) },
            { "FireSpell", new Tuple<MonsterType, ElementType>(MonsterType.None, ElementType.Fire) },
            { "WaterSpell", new Tuple<MonsterType, ElementType>(MonsterType.None, ElementType.Water) },
            { "RegularSpell", new Tuple<MonsterType, ElementType>(MonsterType.None, ElementType.Normal) },
        };

        public AddPackageCommand(IGameManager gameManager, CardModel[] cards)
        {
            _gameManager = gameManager;
            _cards = cards;
        }

        public override Response Execute()
        {
            if (User.Username != "admin")
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = "Only the admin can add new packages."
                };
            }

            if (_cards == null)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "Error deserializing JSON. Please check format."
                };
            }

            if (_cards.Length != 5)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "A new package must consist of exactly 5 cards."
                };
            }

            foreach (CardModel card in _cards) {
                if (card.Id == "" || card.Name == "")
                {
                    return new Response()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Payload = "One or more values are empty."
                    };
                }
            }

            var ids = new string[5];
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Tuple<MonsterType, ElementType> types = _cardMappings[_cards[i].Name];
                    if (types.Item1 == MonsterType.None)
                    {
                        _gameManager.AddCard(new Spell(_cards[i].Id, _cards[i].Name, types.Item2, (int)_cards[i].Damage));
                    } else
                    {
                        _gameManager.AddCard(new Monster(_cards[i].Id, _cards[i].Name, types.Item2, (int)_cards[i].Damage, types.Item1));
                    }
                } catch (KeyNotFoundException)
                {
                    return new Response()
                    {
                        StatusCode = StatusCode.BadRequest,
                        Payload = $"No card with the name '{_cards[i].Name}' exists."
                    };
                } catch (ArgumentException)
                {
                    return new Response()
                    {
                        StatusCode = StatusCode.Conflict,
                        Payload = $"A card with ID {_cards[i].Id} already exists."
                    };
                }
                ids[i] = _cards[i].Id;
            }

            _gameManager.BundlePackage(ids);

            return new Response()
            {
                StatusCode = StatusCode.Created
            };
        }
    }
}
