using System.Diagnostics;
using System.Timers;

namespace GcLab
{
    public class TimerTest : IThreadingLab
    {
        private readonly Timer _t;

        public TimerTest()
        {
            _t = new Timer(300);
            _t.Elapsed += Func;
        }

        public void Start()
        {
            _t.Start();
        }

        private void Func(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(GetType().Name);
        }

        public void Stop()
        {
            _t.Stop();
        }
    }
}
