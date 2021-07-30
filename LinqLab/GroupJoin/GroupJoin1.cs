using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqLab.BaseModel;

namespace LinqLab.GroupJoin
{
    public class GroupJoin1
    {
        public void Test()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
                new Student() { StudentID = 4, StudentName = "Ram",  StandardID =2 },
                new Student() { StudentID = 5, StudentName = "Ron" }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };

            var groupJoin = standardList.GroupJoin(studentList,  //inner sequence
                std => std.StandardID, //outerKeySelector 
                s => s.StandardID,     //innerKeySelector
                (std, studentsGroup) => new // resultSelector 
                {
                    Students = studentsGroup,
                    StandarFulldName = std.StandardName
                });

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandarFulldName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }

            Console.WriteLine();
            var groupJoin2 = from std in standardList
                join s in studentList
                    on std.StandardID equals s.StandardID
                    into studentGroup
                select new
                {
                    Students = studentGroup,
                    StandardName = std.StandardName
                };

            foreach (var item in groupJoin2)
            {
                Console.WriteLine(item.StandardName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }
        }
    }
}
