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
        private readonly IMessageManager messageManager;
        public Credentials Credentials { get; private set; }

        public RegisterCommand(IMessageManager messageManager, Credentials credentials)
        {
            Credentials = credentials;
            this.messageManager = messageManager;
        }

        public Response Execute()
        {
            var response = new Response();
            try
            {
                messageManager.RegisterUser(Credentials);
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
