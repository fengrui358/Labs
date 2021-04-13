﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextLab.Entities
{
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

        public List<Post> Posts { get; set; }
    }
}
