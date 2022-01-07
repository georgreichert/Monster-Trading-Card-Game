using Server.Core.Authentication;
using Server.Core.Request;
using Server.DAL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class IdentityProvider : IIdentityProvider
    {
        private readonly IUserRepository userRepository;

        public IdentityProvider(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IIdentity GetIdentyForRequest(RequestContext request)
        {
            User currentUser = null;

            if (request.Header.TryGetValue("Authorization", out string authToken))
            {
                const string prefix = "Basic ";
                if (authToken.StartsWith(prefix))
                {
                    currentUser = userRepository.GetUserByAuthToken(authToken.Substring(prefix.Length));
                }
            }

            return currentUser;
        }
    }
}
