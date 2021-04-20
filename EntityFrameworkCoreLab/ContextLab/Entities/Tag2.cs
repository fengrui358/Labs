using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContextLab.Entities
{
    public class Tag2
    {
        public string Id { get; set; }
        public virtual List<PostTag2> PostTags2 { get; set; }
    }
}
