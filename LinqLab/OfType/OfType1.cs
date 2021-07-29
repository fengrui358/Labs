using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqLab.BaseModel;

namespace LinqLab.OfType
{
    public class OfType1
    {
        public void Test()
        {
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            var stringResult = from s in mixedList.OfType<string>()
                select s;

            Console.WriteLine((nameof(stringResult)));
            foreach (var s in stringResult)
            {
                Console.WriteLine(s);
            }
            
            Console.WriteLine();

            var intResult = from s in mixedList.OfType<int>()
                select s;
            Console.WriteLine(nameof(intResult));
            foreach (var s in intResult)
            {
                Console.WriteLine(s);
            }
        }
    }
}
