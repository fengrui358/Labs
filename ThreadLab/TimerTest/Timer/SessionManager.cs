using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    class SessionManager
    {
        private ConcurrentDictionary<Guid, Session> _sessions = new ConcurrentDictionary<Guid, Session>();
        private System.Threading.Thread _addThreading;

        public SessionManager()
        {
            for (int i = 0; i < 1000000; i++)
            {
                var session = new Session();
                session.ExpireEvent += ExpireEventHandler;

                _sessions.TryAdd(session.Id, session);
            }

            //启动一个测试插入线程
            _addThreading = new System.Threading.Thread(Add);
            _addThreading.Start();
        }

        private void ExpireEventHandler(object sender, EventArgs eventArgs)
        {
            Session outSession;

            if (!_sessions.TryRemove(((Session) sender).Id, out outSession))
            {
                Debug.WriteLine("移除失败");
            }
            else
            {
                //Debug.WriteLine("移除成功，剩余" + _sessions.Count);
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
