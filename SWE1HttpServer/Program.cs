using System;
using System.Net;
using Newtonsoft.Json;
using Server.Core.Request;
using Server.Core.Routing;
using Server.Core.Server;
using Server.DAL;
using Server.Models;
using Server.RouteCommands.Game;
using Server.RouteCommands.Users;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new Database("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=mtcgdb");
            var gameManager = new GameManager(database.CardRepository, database.UserRepository, new BattleManager());

            var identityProvider = new IdentityProvider(database.UserRepository);
            var routeParser = new RouteParser();

            var router = new Router(routeParser, identityProvider);
            RegisterRoutes(router, gameManager);

            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();
        }

        private static void RegisterRoutes(Router router, IGameManager gameManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions", (r, p) => new LoginCommand(gameManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users", (r, p) => new RegisterCommand(gameManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Get, "/score", (r, p) => new ShowScoreboardCommand(gameManager));

            // protected routes
            router.AddProtectedRoute(HttpMethod.Post, "/packages", (r, p) => new AddPackageCommand(gameManager, Deserialize<CardModel[]>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Post, "/transactions/packages", (r, p) => new AcquirePackageCommand(gameManager));
            router.AddProtectedRoute(HttpMethod.Get, "/cards", (r, p) => new ShowCardsCommand(gameManager));
            router.AddProtectedRoute(HttpMethod.Get, "/stats", (r, p) => new ShowStatsCommand(gameManager));
            router.AddProtectedRoute(HttpMethod.Post, "/battles", (r, p) => new BattleRequestCommand(gameManager));
            router.AddProtectedRoute(HttpMethod.Get, "/deck", (r, p) => new ShowDeckCommand(gameManager, p));
            router.AddProtectedRoute(HttpMethod.Get, "/tradings", (r, p) => new ShowTradingsCommand(gameManager));
            router.AddProtectedRoute(HttpMethod.Delete, "/tradings/{id}", (r, p) => new DeleteTradingCommand(gameManager, p["params"]["id"]));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings/{id}", (r, p) => new TradeCommand(gameManager, p["params"]["id"], Deserialize<string>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings", (r, p) => new AddTradingCommand(gameManager, Deserialize<Trading>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Put, "/deck", (r, p) => new ConfigureDeckCommand(gameManager, Deserialize<string[]>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/users/{username}", (r, p) => new ShowUserDataCommand(gameManager, p["params"]["username"]));
            router.AddProtectedRoute(HttpMethod.Put, "/users/{username}", (r, p) => new SetUserDataCommand(gameManager, p["params"]["username"], Deserialize<UserPublicData>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Post, "/sales", (r, p) => new AddSaleCommand(gameManager, Deserialize<Sale>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Delete, "/sales/{id}", (r, p) => new DeleteSaleCommand(gameManager, p["params"]["id"]));
            router.AddProtectedRoute(HttpMethod.Post, "/sales/{id}", (r, p) => new BuyCommand(gameManager, p["params"]["id"]));
            router.AddProtectedRoute(HttpMethod.Get, "/sales", (r, p) => new ShowSalesCommand(gameManager));
        }

        private static T Deserialize<T>(string payload) where T : class
        {
            T deserializedData;
            try
            {
                deserializedData = JsonConvert.DeserializeObject<T>(payload);
            } catch (Exception)
            {
                deserializedData = null;
            }
            return deserializedData;
        }
    }
}
