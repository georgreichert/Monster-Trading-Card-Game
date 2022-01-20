using Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class DeleteSaleCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string _id;

        public DeleteSaleCommand(IGameManager gameManager, string id)
        {
            _gameManager = gameManager;
            _id = id;
        }

        public override Response Execute()
        {
            try
            {
                _gameManager.DeleteSale(_id, User.Username);
            }
            catch (ArgumentException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = e.Message
                };
            }
            catch (UnauthorizedAccessException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = e.Message
                };
            }
            catch (KeyNotFoundException)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = $"No sale with ID {_id} was found."
                };
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok
            };
        }
    }
}
