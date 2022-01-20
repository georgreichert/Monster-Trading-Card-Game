using Newtonsoft.Json;
using Server.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class ShowSalesCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        public ShowSalesCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(_gameManager.GetSales())
            };
        }
    }
}
