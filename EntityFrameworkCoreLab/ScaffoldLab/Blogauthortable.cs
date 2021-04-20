using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    /// <summary>
    /// 作者
    /// </summary>
    public partial class Blogauthortable
    {
        public Blogauthortable()
        {
            Blogs = new HashSet<Blog>();
        }

        public long Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
