using Server.Core.Response;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class BuyCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string _id;

        public BuyCommand(IGameManager gameManager, string id)
        {
            _gameManager = gameManager;
            _id = id;
        }

        public override Response Execute()
        {
            try
            {
                _gameManager.Buy(_id, User.Username);
            }
            catch (ArgumentException e)
            {
                return new()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = e.Message
                };
            }
            catch (KeyNotFoundException e)
            {
                return new()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = e.Message
                };
            }
            catch (NotEnoughCoinsException e)
            {
                return new()
                {
                    StatusCode = StatusCode.NoContent,
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
