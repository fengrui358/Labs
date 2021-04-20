using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Posttag
    {
        public long PostId { get; set; }
        public string TagId { get; set; }
        public DateTime PublicationDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
