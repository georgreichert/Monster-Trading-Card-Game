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
    class LoginCommand : IRouteCommand
    {
        private readonly IGameManager gameManager;

        public Credentials Credentials { get; private set; }

        public LoginCommand(IGameManager gameManager, Credentials credentials)
        {
            Credentials = credentials;
            this.gameManager = gameManager;
        }

        public Response Execute()
        {
            if (Credentials == null || Credentials.Username == null || Credentials.Password == null)
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "Error deserializing JSON. Please check format."
                };
            }

            User user;
            try
            {
                user = gameManager.LoginUser(Credentials);
            }
            catch (UserNotFoundException)
            {
                user = null;
            }

            var response = new Response();
            if (user == null)
            {
                response.StatusCode = StatusCode.Unauthorized;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = user.Token;
            }

            return response;
        }
    }
}
