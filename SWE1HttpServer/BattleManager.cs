using MTCG.Cards;
using MTCG.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class BattleManager : IBattleManager
    {
        private readonly Queue<Tuple<string, Deck>> _queue = new();
        private bool _running = false;
        private readonly Dictionary<string, Tuple<BattleResult, List<string>>> _logs = new();

        public void EnterQueue(string username, Deck deck)
        {
            _queue.Enqueue(new(username, deck));
        }

        public Tuple<BattleResult, List<string>> GetBattleLog(string username)
        {
            while (_running) {
                try
                {
                    // TODO: MUTEX
                    var log = _logs[username];
                    _logs.Remove(username);
                    return log;
                    //endmutex
                } 
                catch (KeyNotFoundException) 
                {
                    Thread.Sleep(500);
                } 
            }
            return null;
        }

        private void HandleQueue()
        {
            while (_running)
            {
                if (_queue.Count >= 2)
                {
                    // TODO: MUTEX
                    var player1 = _queue.Dequeue();
                    var player2 = _queue.Dequeue();
                    // endmutex
                    GameController game = new(player1.Item2, player2.Item2);
                    Result result = game.Play();
                    BattleResult player1Result = result == Result.Player1Win ? BattleResult.Win :
                        (result == Result.Player2Win ? BattleResult.Lose : BattleResult.Draw);
                    BattleResult player2Result = result == Result.Player1Win ? BattleResult.Lose :
                        (result == Result.Player2Win ? BattleResult.Win : BattleResult.Draw);
                    _logs.Add(player1.Item1, new(player1Result, game.BattleLog));
                    _logs.Add(player2.Item1, new (player2Result, game.BattleLog));
                }
            }
        }

        public void Start()
        {
            _running = true;
            Thread t = new(HandleQueue);
            t.Start();
        }

        public void Stop()
        {
            _running = false;
        }
    }
}
