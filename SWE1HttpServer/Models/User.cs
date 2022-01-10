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
    }
}
