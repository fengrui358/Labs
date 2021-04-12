using System.Collections.Generic;

namespace ContextLab.Entities
{
    public class Author
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
