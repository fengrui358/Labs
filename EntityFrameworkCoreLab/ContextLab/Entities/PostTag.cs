using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextLab.Entities
{
    public class PostTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PublicationDate { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
