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
        private readonly Dictionary<string, Dictionary<string, string>> _parameters;

        public ShowDeckCommand(IGameManager gameManager, Dictionary<string, Dictionary<string, string>> parameters)
        {
            _gameManager = gameManager;
            _parameters = parameters;
        }

        public override Response Execute()
        {
            Deck deck = _gameManager.GetDeck(User.Username);
            string payload = $"###############\n" +
                $"Name: {deck.Name}\n";
            bool plain = false;

            if (_parameters["urlParams"].ContainsKey("format") && _parameters["urlParams"]["format"] == "plain")
            {
                plain = true;
            }

            List<CardModel> cards = new();
            foreach (Card card in deck.GetCards())
            {
                if (plain)
                {
                    payload += $"{card.Name} - Type: {card.EType}, Damage: {card.Damage}\n";
                }
                else
                {
                    cards.Add(CardModel.FromCard(card));
                }
            }
            payload += "###############\n";

            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = plain ? payload : JsonConvert.SerializeObject(cards)
            };
        }
    }
}
