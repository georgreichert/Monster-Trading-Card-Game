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
            Response r;
            try
            {
                _gameManager.GiveRandomPackageToUser(User.Username);
                r = new()
                {
                    StatusCode = StatusCode.Ok
                };
            } catch (NoPackagesException e)
            {
                r = new()
                {
                    StatusCode = StatusCode.NoContent,
                    Payload = e.Message
                };
            }
            return r;
        }
    }
}
