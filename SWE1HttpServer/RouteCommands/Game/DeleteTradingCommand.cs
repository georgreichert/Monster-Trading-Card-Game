using Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class DeleteTradingCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string _id;

        public DeleteTradingCommand(IGameManager gameManager, string id)
        {
            _gameManager = gameManager;
            _id = id;
        }

        public override Response Execute()
        {
            try
            {
                _gameManager.DeleteTrading(_id, User.Username);
            } catch (ArgumentException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = e.Message
                };
            } catch (UnauthorizedAccessException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = e.Message
                };
            } catch (KeyNotFoundException)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = $"No trading with ID {_id} was found."
                };
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok
            };
        }
    }
}
