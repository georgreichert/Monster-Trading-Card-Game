using System;
using System.Net;
using Newtonsoft.Json;
using Server.Core.Request;
using Server.Core.Routing;
using Server.Core.Server;
using Server.DAL;
using Server.Models;
using Server.RouteCommands.Messages;
using Server.RouteCommands.Users;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var messageRepository = new InMemoryMessageRepository();
            var userRepository = new InMemoryUserRepository();
            var messageManager = new MessageManager(messageRepository, userRepository);

            var identityProvider = new MessageIdentityProvider(userRepository);
            var routeParser = new IdRouteParser();

            var router = new Router(routeParser, identityProvider);
            RegisterRoutes(router, messageManager);

            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();
        }

        private static void RegisterRoutes(Router router, IMessageManager messageManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions", (r, p) => new LoginCommand(messageManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users", (r, p) => new RegisterCommand(messageManager, Deserialize<Credentials>(r.Payload)));

            // protected routes
            router.AddProtectedRoute(HttpMethod.Get, "/messages", (r, p) => new ListMessagesCommand(messageManager));
            router.AddProtectedRoute(HttpMethod.Post, "/messages", (r, p) => new AddMessageCommand(messageManager, r.Payload));
            router.AddProtectedRoute(HttpMethod.Get, "/messages/{id}", (r, p) => new ShowMessageCommand(messageManager, int.Parse(p["id"])));
            router.AddProtectedRoute(HttpMethod.Put, "/messages/{id}", (r, p) => new UpdateMessageCommand(messageManager, int.Parse(p["id"]), r.Payload));
            router.AddProtectedRoute(HttpMethod.Delete, "/messages/{id}", (r, p) => new RemoveMessageCommand(messageManager, int.Parse(p["id"])));
        }

        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }
    }
}
