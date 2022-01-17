using MTCG.Cards;
using MTCG.Cards.Monsters;
using MTCG.Cards.Spells;
using Npgsql;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Server.DAL
{
    public class DatabaseRepository : IUserRepository, ICardRepository
    {
        private NpgsqlConnection _connection;
        private static readonly object _lock = new object();
        private const string _insertUser = "INSERT INTO \"Users\"(username,password,name,bio,image) VALUES(@username,@password,'','','')";
        private const string _getUserByCredentials = "SELECT * FROM \"Users\" WHERE username = @username AND password = @password";
        private const string _getUserByAuthToken = "SELECT * FROM \"Auth_tokens\" JOIN \"Users\" USING(username) WHERE token = @token";
        private const string _loginUser = "INSERT INTO \"Auth_tokens\"(username,token) VALUES(@username,@token)";
        private const string _insertCard = "INSERT INTO \"Cards\"(id,name,etype,mtype,dmg) VALUES(@id,@name,@etype,@mtype,@dmg)";
        private const string _bundlePackage = "INSERT INTO \"Packages\"(card1,card2,card3,card4,card5) VALUES(@card1,@card2,@card3,@card4,@card5)";
        private const string _getUserByName = "SELECT * FROM \"Users\" WHERE username = @username";
        private const string _updateUser = "UPDATE \"Users\" SET name = @name, bio = @bio, image = @image, wins = @wins, losses = @losses, draws = @draws, elo = @elo, coins = @coins WHERE username = @username";
        private const string _getNextPackage = "SELECT * FROM \"Packages\" ORDER BY id ASC LIMIT 1";
        private const string _deletePackage = "DELETE FROM \"Packages\" WHERE id = @id";
        private const string _giveCardToUser = "INSERT INTO \"Users_own_Cards\"(username,card) VALUES(@username,@card)";
        private const string _getCardsByUser = "SELECT * FROM \"Cards\" JOIN \"Users_own_Cards\" ON id = card WHERE username = @username";
        private const string _getCardById = "SELECT * FROM \"Cards\" WHERE id = @id";
        private const string _getDeck = "SELECT * FROM \"Cards\" JOIN \"Cards_in_Decks\" ON id = card WHERE username = @username";
        private const string _checkUserOwnsCard = "SELECT * FROM \"Users_own_Cards\" WHERE username = @username AND card = @card";
        private const string _removeDeck = "DELETE FROM \"Cards_in_Decks\" WHERE username = @username";
        private const string _checkForCardInTradings = "SELECT * FROM \"Tradings\" WHERE card = @card";
        private const string _assignCardToDeck = "INSERT INTO \"Cards_in_Decks\"(username,card) VALUES(@username,@card)";
        private const string _getUserByCard = "SELECT * FROM \"Users\" JOIN \"Users_own_Cards\" USING(username) WHERE card = @card";
        private const string _getScoreBoard = "SELECT username, elo FROM \"Users\" ORDER BY elo DESC";
        private const string _checkForCardInDeck = "SELECT * FROM \"Cards_in_Decks\" WHERE card = @card";
        private const string _getTradings = "SELECT * FROM \"Tradings\"";
        private const string _getTrading = "SELECT * FROM \"Tradings\" WHERE id = @id";
        private const string _addTrading= "INSERT INTO \"Tradings\"(id,type,mindmg,card) VALUES(@id,@type,@mindmg,@card)";
        private const string _deleteTrading = "DELETE FROM \"Tradings\" WHERE id = @id";
        private const string _changeCardOwnership = "UPDATE \"Users_own_Cards\" SET username = @username WHERE card = @card";

        public DatabaseRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public void AddTrading(TradingParsed trading)
        {
            lock (_lock)
            {
                try
                {
                    using (var cmd = new NpgsqlCommand(_addTrading, _connection))
                    {
                        cmd.Parameters.AddWithValue("id", trading.Id);
                        cmd.Parameters.AddWithValue("type", TypeMapper.MapMonsterType(trading.Type));
                        cmd.Parameters.AddWithValue("mindmg", trading.MinimumDamage);
                        cmd.Parameters.AddWithValue("card", trading.CardToTrade);
                        cmd.ExecuteNonQuery();
                    }
                } catch (PostgresException)
                {
                    throw new DuplicateTradingException($"A trading with the ID {trading.Id} already exists.");
                }
            }
        }

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
                    break;
            }
            UpdateUser(user);
        }

        public void AssignCardToDeck(string id)
        {
            string username = GetUserByCard(id).Username;
            lock (_lock)
            {
                using var cmd = new NpgsqlCommand(_assignCardToDeck, _connection);
                cmd.Parameters.AddWithValue("card", id);
                cmd.Parameters.AddWithValue("username", username);
                cmd.ExecuteNonQuery();
            }
        }

        public void AssignCardToUser(string id, string user)
        {
            throw new System.NotImplementedException();
        }

        public void BundlePackage(string[] ids)
        {
            lock (_lock)
            {
                using var cmd = new NpgsqlCommand(_bundlePackage, _connection);
                for (int i = 0; i < 5; i++)
                {
                    cmd.Parameters.AddWithValue($"card{i+1}", ids[i]);
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCard(string id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTrading(string id)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_deleteTrading, _connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Card GetCard(string id)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getCardById, _connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return ReadCard(reader);
                    }
                    else
                    {
                        throw new KeyNotFoundException($"No card with ID {id} was found.");
                    }
                }
            }
        }

        public IEnumerable<Card> GetCards(string username)
        {
            lock (_lock)
            {
                List<Card> cards = new();
                using (var cmd = new NpgsqlCommand(_getCardsByUser, _connection))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(ReadCard(reader));
                    }
                }
                return cards;
            }
        }

        public Deck GetDeck(string user)
        {
            lock (_lock)
            {
                Deck deck = new($"{user}'s Deck");
                using (var cmd = new NpgsqlCommand(_getDeck, _connection))
                {
                    cmd.Parameters.AddWithValue("username", user);

                    
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        deck.AddCard(ReadCard(reader));
                    }
                }
                return deck;
            }
        }

        public ScoreboardEntry[] GetScoreBoard()
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getScoreBoard, _connection))
                {
                    using var reader = cmd.ExecuteReader();
                    List<ScoreboardEntry> board = new();
                    while (reader.Read())
                    {
                        board.Add(new ScoreboardEntry()
                        {
                            Name = Convert.ToString(reader["username"]),
                            Score = Convert.ToInt32(reader["elo"])
                        });
                    }
                    return board.ToArray();
                }
            }
        }

        public TradingParsed GetTrading(string id)
        {
            lock (_lock) 
            {
                using (var cmd = new NpgsqlCommand(_getTrading, _connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new TradingParsed()
                        {
                            Id = Convert.ToString(reader["id"]),
                            CardToTrade = Convert.ToString(reader["card"]),
                            MinimumDamage = Convert.ToInt32(reader["mindmg"]),
                            Type = TypeMapper.MapMonsterType(Convert.ToString(reader["type"]))
                        };
                    } else
                    {
                        throw new KeyNotFoundException($"No trading with ID {id} was found.");
                    }
                }
            }
        }

        public Trading[] GetTradings()
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getTradings, _connection))
                {
                    List<Trading> tradings = new();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tradings.Add(new Trading
                        {
                            Id = Convert.ToString(reader["id"]),
                            Type = Convert.ToString(reader["type"]),
                            MinimumDamage = Convert.ToInt32(reader["mindmg"]),
                            CardToTrade = Convert.ToString(reader["card"])
                        });
                    }
                    return tradings.ToArray();
                }
            }
        }

        public User GetUserByAuthToken(string authToken)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getUserByAuthToken, _connection))
                {
                    cmd.Parameters.AddWithValue("token", authToken);

                    
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return ReadUser(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private User GetUserByCard(string id)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getUserByCard, _connection))
                {
                    cmd.Parameters.AddWithValue("card", id);

                    
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return ReadUser(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private User GetUserByName(string username)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_getUserByName, _connection))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return ReadUser(reader);
                    }
                    else
                    {
                        throw new UserNotFoundException($"There is no user named {username}");
                    }
                }
            }
        }

        public UserPublicData GetUserPublicData(string username)
        {
            User user = GetUserByName(username);
            return new UserPublicData()
            {
                Name = user.Name,
                Bio = user.Bio,
                Image = user.Image
            };
        }

        public Stats GetUserStats(string username)
        {
            User user = GetUserByName(username);
            return new Stats()
            {
                Wins = user.Wins,
                Losses = user.Losses,
                Draws = user.Draws,
                ELO = user.ELO
            };
        }

        public void GivePackageToUser(string user)
        {
            lock (_lock)
            {
                string[] cardIds = new string[5];
                int packageId;
                using (var cmd = new NpgsqlCommand(_getNextPackage, _connection))
                {
                    
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            cardIds[i] = Convert.ToString(reader[$"card{i+1}"]);
                        }
                        packageId = Convert.ToInt32(reader["id"]);
                    }
                    else
                    {
                        throw new NoPackagesException("There are no packages left in the database.");
                    }
                }
                if (!TakeCoins(5, user))
                {
                    throw new NotEnoughCoinsException($"User {user} does not have enough coins for this transaction.");
                }
                using (var cmd = new NpgsqlCommand(_deletePackage, _connection))
                {
                    cmd.Parameters.AddWithValue("id", packageId);
                    cmd.ExecuteNonQuery();
                }
                foreach (string id in cardIds)
                {
                    using (var cmd = new NpgsqlCommand(_giveCardToUser, _connection))
                    {
                        cmd.Parameters.AddWithValue("username", user);
                        cmd.Parameters.AddWithValue("card", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void InsertCard(Card card)
        {
            lock (_lock)
            {
                var affectedRows = 0;
                var mType = MonsterType.None;
                Monster monster = card as Monster;
                if (monster != null)
                {
                    mType = monster.MType;
                }
                try
                {
                    using var cmd = new NpgsqlCommand(_insertCard, _connection);
                    cmd.Parameters.AddWithValue("id", card.ID);
                    cmd.Parameters.AddWithValue("name", card.Name);
                    cmd.Parameters.AddWithValue("etype", TypeMapper.MapElementType(card.EType));
                    cmd.Parameters.AddWithValue("mtype", TypeMapper.MapMonsterType(mType));
                    cmd.Parameters.AddWithValue("dmg", card.Damage);
                    affectedRows = cmd.ExecuteNonQuery();
                }
                catch (PostgresException)
                {
                    throw new DuplicateCardException($"A card with the ID {card.ID} is already in the database.");
                }
            }
        }

        public bool InsertUser(User user)
        {
            lock (_lock) 
            {
                var affectedRows = 0;
                try
                {
                    using var cmd = new NpgsqlCommand(_insertUser, _connection);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    affectedRows = cmd.ExecuteNonQuery();
                }
                catch (PostgresException) { }
                return affectedRows > 0; 
            }
        }

        public bool IsCardInDeck(string id)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_checkForCardInDeck, _connection))
                {
                    cmd.Parameters.AddWithValue("card", id);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public bool IsCardInTrading(string id)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_checkForCardInTradings, _connection))
                {
                    cmd.Parameters.AddWithValue("card", id);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public bool IsOwner(string[] ids, string user)
        {
            lock (_lock)
            {
                foreach (string id in ids)
                {
                    using (var cmd = new NpgsqlCommand(_checkUserOwnsCard, _connection))
                    {
                        cmd.Parameters.AddWithValue("username", user);
                        cmd.Parameters.AddWithValue("card", id);

                        
                        using var reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public User LoginUser(string username, string password)
        {
            lock (_lock)
            {
                User user = null;
                using (var cmd = new NpgsqlCommand(_getUserByCredentials, _connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);

                    
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = ReadUser(reader);
                    }
                }
                if (user != null)
                {
                    try
                    {
                        var tokenCmd = new NpgsqlCommand(_loginUser, _connection);
                        tokenCmd.Parameters.AddWithValue("username", username);
                        tokenCmd.Parameters.AddWithValue("token", user.Token);
                        tokenCmd.ExecuteNonQuery();
                    }
                    catch (PostgresException) { }
                }
                return user;
            }
        }

        private Card ReadCard(IDataRecord record)
        {
            string id = Convert.ToString(record["id"]);
            string name = Convert.ToString(record["name"]);
            ElementType eType = TypeMapper.MapElementType(Convert.ToString(record["etype"]));
            MonsterType mType = TypeMapper.MapMonsterType(Convert.ToString(record["mtype"]));
            int dmg = Convert.ToInt32(record["dmg"]);
            Card card;
            if (mType == MonsterType.None)
            {
                card = new Spell(id, name, eType, dmg);
            } else
            {
                card = new Monster(id, name, eType, dmg, mType);
            }
            return card;
        }

        private User ReadUser(IDataRecord record)
        {
            int wins = Convert.ToInt32(record["wins"]);
            int losses = Convert.ToInt32(record["losses"]);
            int draws = Convert.ToInt32(record["draws"]);
            int elo = Convert.ToInt32(record["elo"]);
            var user = new User(wins, losses, draws, elo)
            {
                Username = Convert.ToString(record["username"]),
                Password = Convert.ToString(record["password"]),
                Name = Convert.ToString(record["name"]),
                Bio = Convert.ToString(record["bio"]),
                Image = Convert.ToString(record["image"]),
                Coins = Convert.ToInt32(record["coins"]),
            };
            return user;
        }

        public void RemoveCardFromDeck(string id)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDeck(string user)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_removeDeck, _connection))
                {
                    cmd.Parameters.AddWithValue("username", user);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SetUserPublicData(string username, UserPublicData data)
        {
            User user = GetUserByName(username);
            user.Name = data.Name;
            user.Bio = data.Bio;
            user.Image = data.Image;
            UpdateUser(user);
        }

        public bool TakeCoins(int coins, string username)
        {
            User user = GetUserByName(username);
            if (user.Coins >= coins)
            {
                user.Coins -= coins;
                UpdateUser(user);
                return true;
            }
            return false;
        }

        public void Trade(string tradingId, string cardToTrade)
        {
            TradingParsed trading = GetTrading(tradingId);
            Card card1 = GetCard(cardToTrade);
            Card card2 = GetCard(trading.CardToTrade);
            Monster monster = card1 as Monster;
            if (trading.MinimumDamage <= card1.Damage)
            {
                if ((trading.Type == MonsterType.None && monster == null)
                    || (trading.Type == MonsterType.Any && monster != null))
                {
                    lock (_lock)
                    {
                        User user1 = GetUserByCard(card1.ID);
                        User user2 = GetUserByCard(card2.ID);
                        using (var cmd = new NpgsqlCommand(_changeCardOwnership, _connection))
                        {
                            cmd.Parameters.AddWithValue("username", user1.Username);
                            cmd.Parameters.AddWithValue("card", card2.ID);
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new NpgsqlCommand(_changeCardOwnership, _connection))
                        {
                            cmd.Parameters.AddWithValue("username", user2.Username);
                            cmd.Parameters.AddWithValue("card", card1.ID);
                            cmd.ExecuteNonQuery();
                        }
                        DeleteTrading(tradingId);
                    }
                } else
                {
                    throw new ArgumentException($"Type requirement not fulfilled, your card was rejected.");
                }
            } else
            {
                throw new ArgumentException($"Minimum damage requirement not fulfilled, your card was rejected.");
            }
        }

        private void UpdateUser(User user)
        {
            lock (_lock)
            {
                using (var cmd = new NpgsqlCommand(_updateUser, _connection))
                {
                    cmd.Parameters.AddWithValue("name", user.Name);
                    cmd.Parameters.AddWithValue("bio", user.Bio);
                    cmd.Parameters.AddWithValue("image", user.Image);
                    cmd.Parameters.AddWithValue("wins", user.Wins);
                    cmd.Parameters.AddWithValue("losses", user.Losses);
                    cmd.Parameters.AddWithValue("draws", user.Draws);
                    cmd.Parameters.AddWithValue("coins", user.Coins);
                    cmd.Parameters.AddWithValue("elo", user.ELO);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}