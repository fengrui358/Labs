using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ContextLab.Entities
{
    [Comment("作者")]
    public class Author
    {
        public long Id { get; set; }

        [Comment("名字")]
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }
    }
}
