using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextLab.Entities
{
    [Table("other-entity")]
    public class Other
    {
        public Guid Id { get; set; }

        [Column("change_new_column_name")]
        [Required]
        public string TestString { get; set; }
    }
}
