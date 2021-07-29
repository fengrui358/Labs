using System;
using System.Collections.Generic;
using System.Linq;
using LinqLab.BaseModel;

namespace LinqLab.OrderBy
{
    public class OrderBy
    {
        public void Test()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 },
                new Student() { StudentID = 6, StudentName = "Ram" , Age = 18 }
            };

            var orderByResult = from s in studentList
                orderby s.StudentName ascending , s.Age descending 
                select new { s.StudentName, s.Age };

            foreach (var x1 in orderByResult)
            {
                Console.WriteLine($"{nameof(x1.StudentName)}:{x1.StudentName}   {nameof(x1.Age)}:{x1.Age}");
            }
        }

        public class CompareAge : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x - y;
            }
        }
    }
}
