using System.Collections.Generic;

namespace ContextLab.Entities
{
    public class Tag
    {
        public string TagId { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
    }
}
