using System;
using System.Diagnostics;
using System.Threading;

namespace GcLab
{
    public class ThreadingTimerTest : IThreadingLab
    {
        private readonly Timer _t;

        public ThreadingTimerTest()
        {
            _t = new Timer(Func, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void Func(object obj)
        {
            Debug.WriteLine(GetType().Name);
        }

        public void Start()
        {
            _t.Change(TimeSpan.FromMilliseconds(300), TimeSpan.FromMilliseconds(300));
        }

        public void Stop()
        {
            _t.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
