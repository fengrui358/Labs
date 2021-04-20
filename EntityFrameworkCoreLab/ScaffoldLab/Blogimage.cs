using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Blogimage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
