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
    }
}
