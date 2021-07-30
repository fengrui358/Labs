using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqLab.BaseModel;

namespace LinqLab.GroupBy
{
    public class GroupBy1
    {
        public void Test()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Abram",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 21 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };

            var groupResult = studentList.GroupBy(s => s.Age).GroupBy(x => x.GroupBy(c => c.StudentName));
            foreach (var grouping in groupResult)
            {
                Console.WriteLine($"{nameof(grouping)} count: {grouping.Count()}");
                foreach (var students in grouping)
                {
                    Console.WriteLine($"{nameof(students)} count: {students.Count()}");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{nameof(grouping.Key)}:{students.Key}:{student.StudentName}:{student.Age}");
                    }
                }
            }
        }
    }
}
