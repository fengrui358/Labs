using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GcLab
{
    public class TaskTest : IThreadingLab
    {
        private bool _stopContinue;

        public void Start()
        {
            Task.Run(() => Func());
        }

        private void Func()
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
