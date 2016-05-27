using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueLab
{
    public class User
    {
        public virtual string Uuid { get; set; }

        public virtual string OpenId { get; set; }

        public virtual string Name { get; set; }

        public virtual bool IsManager { get; set; }

        public virtual bool IsFollower { get; set; }

        public User()
        {
            Uuid = Guid.NewGuid().ToString();
            OpenId = Guid.NewGuid().ToString();
            Name = Guid.NewGuid().ToString();

            IsManager = true;
        }
    }
}
