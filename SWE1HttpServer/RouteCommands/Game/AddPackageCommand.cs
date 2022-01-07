using MTCG.Cards;
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
                if (card.Id == "" || card.Name == "" || card.Damage == "")
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
                _gameManager.AddCard(new Spell(_cards[i].Id, _cards[i].Name, ElementType.Fire, Int32.Parse(_cards[i].Damage)));
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
