using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public User GetUserByAuthToken(string authToken)
        {
            return _users.SingleOrDefault(u => u.Token == authToken);
        }

        public User GetUserByCredentials(string username, string password)
        {
            return _users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        public UserPublicData GetUserPublicData(string username)
        {
            User data = _users.SingleOrDefault(u => u.Username == username);
            if (data == null)
            {
                throw new UserNotFoundException($"No user with the name {username} was found.");
            }
            return data.PublicData;
        }

        public bool InsertUser(User user)
        {
            var inserted = false;

            if (GetUserByName(user.Username) == null)
            {
                _users.Add(user);
                inserted = true;
            }

            return inserted;
        }

        public void SetUserPublicData(string username, UserPublicData data)
        {
            User user = _users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new UserNotFoundException($"No user with the name { username } was found.");
            }
            user.PublicData = data;
        }

        private User GetUserByName(string username)
        {
            return _users.SingleOrDefault(u => u.Username == username);
        }
    }
}
