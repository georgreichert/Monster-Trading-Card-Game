using Newtonsoft.Json;
using Server.Core.Response;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ShowStatsCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;

        public ShowStatsCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            Stats stats = _gameManager.GetStats(User.Username);
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(stats)
            };
        }
    }
}
