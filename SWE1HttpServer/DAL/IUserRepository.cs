using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    public interface IUserRepository
    {
        User GetUserByCredentials(string username, string password);

        User GetUserByAuthToken(string authToken);

        bool InsertUser(User user);
        UserPublicData GetUserPublicData(string username);
        void SetUserPublicData(string username, UserPublicData data);
    }
}
