using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExtensionsDemo
{
    public class ReadUnread
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public TestEnum DataType { get; set; }
        public string Key { get; set; }
        public System.DateTime ReadTime { get; set; }
    }

    public enum TestEnum
    {
        A = 0,
        B = 1
    }
}
