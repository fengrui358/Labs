using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextLab.Entities
{
    public class PostTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PublicationDate { get; set; }
        public long PostId { get; set; }
        public virtual Post Post { get; set; }
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
