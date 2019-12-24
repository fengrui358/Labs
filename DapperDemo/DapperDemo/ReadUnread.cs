using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperDemo
{
    [Table("ReadUnread")]
    public class ReadUnread
    {
        public string UserId { get; set; }
        public int DataType { get; set; }
        public string Key { get; set; }
        public DateTime ReadTime { get; set; }
    }
}
