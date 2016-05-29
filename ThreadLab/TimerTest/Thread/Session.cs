using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread
{
    class Session
    {
        public Guid Id { get; set; }

        public DateTime LastRefreshTime { get; set; }

        public Session()
        {
            Id = Guid.NewGuid();

            LastRefreshTime = DateTime.Now;
        }
    }
}
