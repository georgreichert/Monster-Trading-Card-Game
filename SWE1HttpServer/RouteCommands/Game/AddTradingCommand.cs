using Server.Core.Response;
using Server.DAL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class AddTradingCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly Trading _trading;
        
        public AddTradingCommand(IGameManager gameManager, Trading trading)
        {
            _gameManager = gameManager;
            _trading = trading;
        }

        public override Response Execute()
        {
            if (_trading == null)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "Error deserializing JSON. Please check format."
                };
            }

            try
            {
                _gameManager.AddTrading(_trading, User.Username);
            } catch (ArgumentException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = e.Message
                };
            } catch (UnauthorizedAccessException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = e.Message
                };
            } catch (DuplicateTradingException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Conflict,
                    Payload = e.Message
                };
            } catch (KeyNotFoundException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = e.Message
                };
            }

            return new Response()
            {
                StatusCode = StatusCode.Created
            };
        }
    }
}
