using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread
{
    class SessionManager
    {
        private ConcurrentDictionary<Guid, Session> _sessions = new ConcurrentDictionary<Guid, Session>();
        private System.Threading.Thread _refreshThreading;
        private System.Threading.Thread _addThreading;

        public SessionManager()
        {
            for (int i = 0; i < 1000000; i++)
            {
                var session = new Session();
                _sessions.TryAdd(session.Id, session);
            }

            _refreshThreading = new System.Threading.Thread(Refresh);
            _refreshThreading.Start();

            //启动一个测试插入线程
            _addThreading = new System.Threading.Thread(Add);
            _addThreading.Start();
        }

        private void Refresh()
        {
            while (true)
            {
                var expires = _sessions.Where(s => s.Value.LastRefreshTime.AddMinutes(1) < DateTime.Now).ToList();
                foreach (var keyValuePair in expires)
                {
                    Session outSession;
                    _sessions.TryRemove(keyValuePair.Key, out outSession);
                }

                //5分钟的有效期，校验周期休息1S钟不短吧
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Add()
        {
            while (true)
            {               
                var session = new Session();
                _sessions.TryAdd(session.Id, session);

                GC.Collect();
                System.Threading.Thread.Sleep(10 * 1000);
            }
        }
    }
}
