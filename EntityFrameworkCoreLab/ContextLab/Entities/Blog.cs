using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContextLab.Entities.Enums;
using ContextLab.Entities.Structs;
using Microsoft.EntityFrameworkCore;

namespace ContextLab.Entities
{
    /// <summary>
    /// EFCore p211
    /// </summary>
    [Index(nameof(Url), IsUnique = true, Name = "Index_BlogUrl")]
    public class Blog
    {
        private string _backField;

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

        [Comment("BlogType: 0-Blog; 1-RssBlog")]
        public BlogType BlogType { get; set; }

        public List<Post> Posts { get; set; }

        public BlogImage BlogImage { get; set; }

        public IList<AnnualFinance> Finances { get; set; }

        public string GetBackField()
        {
            return _backField;
        }

        public void SetBackField(string backField)
        {
            if (string.IsNullOrEmpty(backField))
            {
                _backField = backField;
            }
        }
    }
}
