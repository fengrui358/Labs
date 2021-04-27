namespace AspnetCoreWebApiLab.Entities
{
    /// <summary>
    /// 代办事项
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
    }
}
