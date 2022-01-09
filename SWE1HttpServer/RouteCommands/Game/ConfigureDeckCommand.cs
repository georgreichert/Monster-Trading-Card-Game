using Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ConfigureDeckCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string[] _ids;

        public ConfigureDeckCommand(IGameManager gameManager, string[] ids)
        {
            _gameManager = gameManager;
            _ids = ids;
        }

        public override Response Execute()
        {
            try
            {
                _gameManager.ConfigureDeck(_ids, User.Username);
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
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok
            };
        }
    }
}
