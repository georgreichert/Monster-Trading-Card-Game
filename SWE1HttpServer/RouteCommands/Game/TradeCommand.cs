using Server.Core.Response;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class TradeCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string _id;
        private readonly string _cardToTrade;

        public TradeCommand(IGameManager gameManager, string id, string cardToTrade)
        {
            _gameManager = gameManager;
            _id = id;
            _cardToTrade = cardToTrade;
        }

        public override Response Execute()
        {
            if (_cardToTrade == null)
            {
                return new()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "No card to trade was specified or specified in wrong format."
                };
            }
            try
            {
                _gameManager.Trade(_id, _cardToTrade, User.Username);
            } catch (ArgumentException e)
            {
                return new()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = e.Message
                };
            } catch (KeyNotFoundException e)
            {
                return new()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = e.Message
                };
            }
            return new()
            {
                StatusCode = StatusCode.Accepted
            };
        }
    }
}
