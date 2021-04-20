using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Tag
    {
        public Tag()
        {
            Posttags = new HashSet<Posttag>();
        }

        public string TagId { get; set; }

        public virtual ICollection<Posttag> Posttags { get; set; }
    }
}
