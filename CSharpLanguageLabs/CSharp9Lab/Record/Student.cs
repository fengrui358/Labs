using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Lab.Record
{
    public sealed record Student(string First, string Last, int Level) : Person(First, Last)
    {
        public override string ToString()
        {
            return $"{base.ToString()}  来源于重写";
        }

        public string GetName()
        {
            return $"{First}--{Last}";
        }
    }
}
