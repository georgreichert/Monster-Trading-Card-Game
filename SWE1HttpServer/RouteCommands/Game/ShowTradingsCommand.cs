using Newtonsoft.Json;
using Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ShowTradingsCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        public ShowTradingsCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(_gameManager.GetTradings())
            };
        }
    }
}
