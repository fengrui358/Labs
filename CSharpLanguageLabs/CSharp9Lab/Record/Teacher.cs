using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Lab.Record
{
    public record Teacher : Person2
    {
        public string Subject { get; }

        public Teacher(string first, string last, string sub): base(first, last) => Subject = sub;
    }
}
