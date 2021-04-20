using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Post
    {
        public Post()
        {
            Posttag2s = new HashSet<Posttag2>();
            Posttags = new HashSet<Posttag>();
            Tag2s = new HashSet<Tag2>();
        }

        public long PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string Url { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual ICollection<Posttag2> Posttag2s { get; set; }
        public virtual ICollection<Posttag> Posttags { get; set; }
        public virtual ICollection<Tag2> Tag2s { get; set; }
    }
}
