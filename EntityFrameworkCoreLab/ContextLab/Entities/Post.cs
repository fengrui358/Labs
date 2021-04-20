using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContextLab.Entities
{
    public class Post
    {
        public long PostId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500000)]
        public string Content { get; set; }

        public string Url { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public virtual List<PostTag> PostTags { get; set; }

        public virtual List<Tag2> Tags2 { get; set; }

        public virtual List<PostTag2> PostTags2 { get; set; }
    }
}
