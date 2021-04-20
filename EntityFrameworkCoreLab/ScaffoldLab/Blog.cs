using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }
        public long AuthorId { get; set; }
        public double Price { get; set; }
        public decimal PriceDecimal { get; set; }
        /// <summary>
        /// BlogType: 0-Blog; 1-RssBlog
        /// </summary>
        public int BlogType { get; set; }
        public string RssUrl { get; set; }
        public string BackField { get; set; }
        public string Finances { get; set; }

        public virtual Blogauthortable Author { get; set; }
        public virtual Blogimage Blogimage { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
