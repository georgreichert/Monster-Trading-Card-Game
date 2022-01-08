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
    class ShowDeckCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;

        public ShowDeckCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            List<CardModel> cards = new();
            foreach (Card card in _gameManager.GetDeck(User.Username).GetCards())
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
