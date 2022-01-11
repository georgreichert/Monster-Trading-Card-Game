using Server.Core.Response;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Users
{
    class SetUserDataCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly UserPublicData _data;
        private readonly string _username;

        public SetUserDataCommand(IGameManager gameManager, string username, UserPublicData data)
        {
            _gameManager = gameManager;
            _data = data;
            _username = username;
        }

        public override Response Execute()
        {
            if (_data == null)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "Error deserializing JSON. Please check format."
                };
            }

            if (_username != User.Username)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = $"User {User.Username} is not authorized to modify {_username}'s data."
                };
            }
            try
            {
                _gameManager.SetUserPublicData(_username, _data);
            } catch (UserNotFoundException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
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
