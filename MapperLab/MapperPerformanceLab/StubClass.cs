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

        public List<StubSubClass> StringListC { get; set; }
    }

    public class StubSubClass
    {
        public string StringA { get; set; }

        public int IntB { get; set; }
    }
}
