using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timer
{
    class Session
    {
        private DateTime _lastRefreshTime;

        public Guid Id { get; set; }

        public DateTime LastRefreshTime
        {
            get { return _lastRefreshTime; }
            private set
            {
                _lastRefreshTime = value;
                _timer.Change((int) TimeSpan.FromSeconds(40).TotalMilliseconds,
                    Timeout.Infinite);
            }
        }

        public EventHandler ExpireEvent;

        private System.Threading.Timer _timer;

        public Session()
        {
            Id = Guid.NewGuid();

            _timer = new System.Threading.Timer((s) =>
            {
                if (LastRefreshTime.AddSeconds(30) < DateTime.Now)
                {
                    ExpireEvent?.Invoke(this, null);
                }
            });
            LastRefreshTime = DateTime.Now;            
        }
    }
}
