﻿using System.Collections.Generic;

namespace ContextLab.Entities
{
    public class Tag
    {
        public string TagId { get; set; }
        public ICollection<Post> Posts { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
