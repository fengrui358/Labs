using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;

namespace Service
{
    public class HelloWordService : IHelloWord
    {
        public string HelloWord()
        {
            return "Hello Word!!!";
        }

        public string HelloWord(string s)
        {
            return s;
        }
    }
}
