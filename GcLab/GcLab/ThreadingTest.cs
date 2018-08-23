using System;
using System.Diagnostics;
using System.Threading;

namespace GcLab
{
    public class ThreadingTest : IThreadingLab
    {
        private bool _stopContinue;
        private readonly Thread _t;

        public ThreadingTest()
        {
            _t = new Thread(Func);
        }

        private void Func()
        {
            while (!_stopContinue)
            {
                Thread.Sleep(300);
                Debug.WriteLine(GetType().Name);
            }
        }

        public void Start()
        {
            _t.Start();
        }

        public void Stop()
        {
            _stopContinue = true;
        }
    }
}
