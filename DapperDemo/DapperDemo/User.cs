using Dapper;

namespace DapperDemo
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }
    }
}
