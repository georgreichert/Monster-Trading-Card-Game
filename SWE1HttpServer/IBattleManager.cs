using MTCG.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public enum BattleResult
    {
        Win,
        Lose,
        Draw
    }

    interface IBattleManager
    {
        public void EnterQueue(string username, Deck deck);
        public void Start();
        public void Stop();
        public Tuple<BattleResult, List<string>> GetBattleLog(string username);
    }
}
