using MTCG.Cards;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IGameManager
    {
        User LoginUser(Credentials credentials);
        void RegisterUser(Credentials credentials);
        void AddCard(Card card);
        void BundlePackage(string[] ids);
    }
}
