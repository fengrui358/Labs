using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Lab.Record
{
    public record Person
    {
        public string LastName { get; }

        public string FirstName { get; }

        public Person(string first, string last) => (FirstName, LastName) = (first, last);
    }
}
