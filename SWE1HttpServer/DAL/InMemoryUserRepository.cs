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

        public void AlterStats(string username, BattleResult result)
        {
            User user = GetUserByName(username);
            switch (result)
            {
                case BattleResult.Win:
                    user.Win();
                    break;
                case BattleResult.Lose:
                    user.Lose();
                    break;
                case BattleResult.Draw:
                    user.Draw();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public ScoreBoard GetScoreBoard()
        {
            List<ScoreboardEntry> scores = new();
            foreach (User user in _users)
            {
                scores.Add(new ScoreboardEntry()
                {
                    Score = user.ELO,
                    Name = user.Name != null && user.Name != "" ? user.Name : user.Username
                });
            }
            scores.Sort((a, b) => b.Score - a.Score);
            return new()
            {
                Scores = scores.ToArray()
            };
        }

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

        public Stats GetUserStats(string username)
        {
            return GetUserByName(username).Stats;
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
