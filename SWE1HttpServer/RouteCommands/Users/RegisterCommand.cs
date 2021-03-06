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
    class RegisterCommand : IRouteCommand
    {
        private readonly IGameManager gameManager;
        public Credentials Credentials { get; private set; }

        public RegisterCommand(IGameManager gameManager, Credentials credentials)
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

            var response = new Response();
            try
            {
                gameManager.RegisterUser(Credentials);
                response.StatusCode = StatusCode.Created;
            }
            catch (DuplicateUserException)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}
