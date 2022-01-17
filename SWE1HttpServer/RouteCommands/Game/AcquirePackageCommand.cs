using Server.Core.Response;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class AcquirePackageCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;

        public AcquirePackageCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            try
            {
                _gameManager.GivePackageToUser(User.Username);
                return new Response()
                {
                    StatusCode = StatusCode.Ok
                };
            } catch (NoPackagesException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NoContent,
                    Payload = e.Message
                };
            } catch (NotEnoughCoinsException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NoContent,
                    Payload = e.Message
                };
            }
        }
    }
}
