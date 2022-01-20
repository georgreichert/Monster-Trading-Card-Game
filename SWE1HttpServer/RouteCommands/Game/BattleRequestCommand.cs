using Server.Core.Response;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class BattleRequestCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;

        public BattleRequestCommand(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override Response Execute()
        {
            List<string> log;
            try
            {
                log = _gameManager.Battle(User.Username);
            } catch (DeckException e)
            {
                return new Response() 
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = e.Message
                };
            } catch (ApplicationException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Payload = e.Message
                };
            } catch (DuplicateBattleEnqueueException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Conflict,
                    Payload = e.Message
                };
            }

            string payload = "";
            foreach (string line in log)
            {
                payload += $"{line}\n";
            }

            return new Response()
            {
                StatusCode = StatusCode.Ok,
                Payload = payload
            };
        }
    }
}
