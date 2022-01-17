using Newtonsoft.Json;
using Server.Core.Response;
using Server.Core.Routing;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Users
{
    class ShowUserDataCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly string _username;

        public ShowUserDataCommand(IGameManager gameManager, string username)
        {
            _gameManager = gameManager;
            _username = username;
        }

        public override Response Execute()
        {
            if (_username != User.Username)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = $"You are not authorized to view {_username}'s data."
                };
            }
            UserPublicData data;
            try
            {
            data = _gameManager.GetUserPublicData(_username);
            } catch (UserNotFoundException)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = $"No user with the name {_username} was found."
                };
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = JsonConvert.SerializeObject(data)
            };
        }
    }
}
