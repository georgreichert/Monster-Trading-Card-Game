using Server.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User : IIdentity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public int Wins { get; private set; } 
        public int Losses { get; private set; }
        public int Draws { get; private set; }
        public int GamesPlayed { 
            get
            {
                return Wins + Losses + Draws;
            }
        }
        public int ELO { get; private set; }

        public string Token => $"{Username}-mtcgToken";
        public UserPublicData PublicData
        {
            get
            {
                return new UserPublicData()
                {
                    Name = Name,
                    Bio = Bio,
                    Image = Image
                };
            }
            set
            {
                Name = value.Name;
                Bio = value.Bio;
                Image = value.Image;
            }
        }
        public Stats Stats
        {
            get
            {
                return new Stats()
                {
                    Wins = Wins,
                    Losses = Losses,
                    Draws = Draws,
                    GamesPlayed = GamesPlayed,
                    ELO = ELO
                };
            }
        }

        public User(int wins, int losses, int draws, int elo)
        {
            Wins = wins;
            Losses = losses;
            Draws = draws;
            ELO = elo;
        }

        public void Win()
        {
            Wins++;
            ELO += 3;
        }

        public void Lose()
        {
            Losses++;
            ELO -= 5;
            if (ELO < 0)
            {
                ELO = 0;
            }
        }

        public void Draw()
        {
            Draws++;
        }
    }
}
