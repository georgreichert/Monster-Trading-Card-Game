using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Routing
{
    public interface IRouteCommand
    {
        Response.Response Execute();
    }
}
