using Server.Core.Authentication;
using Server.Core.Response;
using Server.Core.Routing;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RouteCommands
{
    abstract class ProtectedRouteCommand : IProtectedRouteCommand
    {
        public IIdentity Identity { get; set; }

        public User User => (User)Identity;

        public abstract Response Execute();
    }
}
