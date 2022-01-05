using Server.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Routing
{
    public interface IRouter
    {
        IRouteCommand Resolve(RequestContext request);
    }
}
