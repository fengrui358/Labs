using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContextLab.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ContextLab.Entities
{
    /// <summary>
    /// EFCore p211
    /// </summary>
    [Index(nameof(Url), IsUnique = true, Name = "Index_BlogUrl")]
    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public long AuthorId { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public Author Author { get; set; }

        public double Price { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PriceDecimal { get; set; }

        public BlogType BlogType { get; set; }

        public List<Post> Posts { get; set; }

        public BlogImage BlogImage { get; set; }
    }
}
