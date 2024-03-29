﻿using System;
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

            var groupResult = studentList.GroupBy(s => s.Age).Select(s => new {Age = s.Key, Students = s})
                .GroupBy(x => x.Students.GroupBy(c => c.StudentName)).Select(s => new {StudentName = s.Key, Students = s});
            foreach (var grouping in groupResult)
            {
                Console.WriteLine($"{nameof(grouping.StudentName)} {grouping.StudentName} count: {grouping.Students.Count()}");
                foreach (var students in grouping.Students)
                {
                    Console.WriteLine($"{nameof(students.Age)} {students} count: {students.Students.Count()}");
                    foreach (var student in students.Students)
                    {
                        Console.WriteLine($"{nameof(grouping.StudentName)}:{students.Age}:{student.StudentName}:{student.Age}");
                    }
                }
            }
        }
    }
}
