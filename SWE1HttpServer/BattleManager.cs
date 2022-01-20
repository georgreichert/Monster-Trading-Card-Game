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
        private static object _queueLock = new object();
        private static object _logLock = new object();

        public void EnterQueue(string username, Deck deck)
        {
            lock (_queueLock)
            {
                _queue.Enqueue(new(username, deck));
            }
        }

        public Tuple<BattleResult, List<string>> GetBattleLog(string username)
        {
            while (_running) {
                try
                {
                    lock (_logLock)
                    {
                        var log = _logs[username];
                        _logs.Remove(username);
                        return log;
                    }
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
                Tuple<string, Deck> player1 = null;
                Tuple<string, Deck> player2 = null;
                lock (_queueLock)
                {
                    if (_queue.Count >= 2)
                    {
                        player1 = _queue.Dequeue();
                        player2 = _queue.Dequeue();
                    }
                }
                if (player1 != null && player2 != null)
                {
                    // start new thread to enable parallel battles
                    Thread t = new Thread(() => {
                        GameController game = new(player1.Item2, player2.Item2);
                        Result result = game.Play();
                        BattleResult player1Result = result == Result.Player1Win ? BattleResult.Win :
                            (result == Result.Player2Win ? BattleResult.Lose : BattleResult.Draw);
                        BattleResult player2Result = result == Result.Player1Win ? BattleResult.Lose :
                            (result == Result.Player2Win ? BattleResult.Win : BattleResult.Draw);
                        lock (_logLock)
                        {
                            _logs.Add(player1.Item1, new(player1Result, game.BattleLog));
                            _logs.Add(player2.Item1, new(player2Result, game.BattleLog));
                        }
                    });
                    t.Start();
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

        public bool IsEnqueued(string username)
        {
            lock (_queueLock)
            {
                foreach (var tuple in _queue)
                {
                    if (tuple.Item1 == username)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
