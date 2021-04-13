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

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
