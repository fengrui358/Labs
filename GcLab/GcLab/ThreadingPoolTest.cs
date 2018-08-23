using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace GcLab
{
    public class ThreadingPoolTest : IThreadingLab
    {
        private bool _stopContinue;

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(Func);
        }

        private void Func(object o)
        {
            while (!_stopContinue)
            {
                Thread.Sleep(300);
                Debug.WriteLine(GetType().Name);
            }
        }

        public void Stop()
        {
            _stopContinue = true;
        }
    }
}
