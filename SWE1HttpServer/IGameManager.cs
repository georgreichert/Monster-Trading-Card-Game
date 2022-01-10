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
        void GiveRandomPackageToUser(string username);
        IEnumerable<Card> GetCards(string username);
        Deck GetDeck(string username);
        Stats GetStats(string username);
        void ConfigureDeck(string[] ids, string username);
        UserPublicData GetUserPublicData(string username);
        void SetUserPublicData(string username, UserPublicData data);
        ScoreBoard GetScoreBoard();
        List<string> Battle(string username);
    }
}
