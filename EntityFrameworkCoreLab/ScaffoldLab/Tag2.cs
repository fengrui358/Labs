using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Tag2
    {
        public Tag2()
        {
            Posttag2s = new HashSet<Posttag2>();
        }

        public string Id { get; set; }
        public long? PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ICollection<Posttag2> Posttag2s { get; set; }
    }
}
