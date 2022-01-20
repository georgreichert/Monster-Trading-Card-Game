using Server.Core.Response;
using Server.DAL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands.Game
{
    class AddSaleCommand : ProtectedRouteCommand
    {
        private readonly IGameManager _gameManager;
        private readonly Sale _sale;

        public AddSaleCommand(IGameManager gameManager, Sale sale)
        {
            _gameManager = gameManager;
            _sale = sale;
        }

        public override Response Execute()
        {
            if (IsErroneousJson(_sale))
            {
                return new Response()
                {
                    StatusCode = StatusCode.BadRequest,
                    Payload = "Error deserializing JSON. Please check format."
                };
            }
            try
            {
                _gameManager.AddSale(_sale, User.Username);
            }
            catch (DuplicateSaleException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Conflict,
                    Payload = e.Message
                };
            }
            catch (UnauthorizedAccessException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Unauthorized,
                    Payload = e.Message
                };
            }
            catch (KeyNotFoundException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.NotFound,
                    Payload = e.Message
                };
            }
            catch (CardBlockedException e)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Forbidden,
                    Payload = e.Message
                };
            }
            return new Response()
            {
                StatusCode = StatusCode.Ok
            };
        }

        private bool IsErroneousJson(Sale sale)
        {
            if (sale == null)
            {
                return true;
            }
            if (sale.Id == null || sale.CardToSell == null)
            {
                return true;
            }
            return false;
        }
    }
}
