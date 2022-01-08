using MTCG.Cards;
using Newtonsoft.Json;
using Server.Core.Response;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ShowCardsCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        public ShowCardsCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            List<CardModel> cards = new();
            foreach (Card card in _gameManager.GetCards(User.Username))
            {
                cards.Add(CardModel.FromCard(card));
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(cards)
            };
        }
    }
}
