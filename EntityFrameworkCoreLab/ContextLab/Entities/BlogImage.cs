namespace ContextLab.Entities
{
    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
