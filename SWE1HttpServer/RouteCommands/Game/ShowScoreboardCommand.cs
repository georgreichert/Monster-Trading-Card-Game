using Newtonsoft.Json;
using Server.Core.Response;
using Server.Core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ShowScoreboardCommand : IRouteCommand
    {
        public IGameManager _gameManager;

        public ShowScoreboardCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public Response Execute()
        {
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(_gameManager.GetScoreBoard())
            };
        }
    }
}
