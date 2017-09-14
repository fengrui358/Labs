using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperLab
{
    public class StubClass
    {
        public string StringA { get; set; }

        public int IntB { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public string Nickname { get; set; }

        public string Phone { get; set; }

        public StubSubClass StubSubClass { get; set; }

        public List<StubSubClass> StringListC { get; set; }

        public Dictionary<string, string> Dictionary { get; set; }
    }

    public class StubSubClass
    {
        public string StringA { get; set; }

        public int IntB { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public string Nickname { get; set; }

        public string Phone { get; set; }
    }
}
