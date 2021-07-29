using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqLab.BaseModel;

namespace LinqLab.Join
{
    public class Join
    {
        public void Test()
        {
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardID =1 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
                new Student() { StudentID = 4, StudentName = "Ram" , StandardID =2 },
                new Student() { StudentID = 5, StudentName = "Ron"  }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 2, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };

            var innerJoin = studentList.Join(// outer sequence 
                standardList,  // inner sequence 
                student => student.StandardID,    // outerKeySelector
                standard => standard.StandardID,  // innerKeySelector
                (student, standard) => new  // result selector
                {
                    StudentName = student.StudentName,
                    StandardName = standard.StandardName
                });

            foreach (var x1 in innerJoin)
            {
                Console.WriteLine(
                    $"{nameof(x1.StudentName)}:{x1.StudentName}  {nameof(x1.StandardName)}:{x1.StandardName}");
            }

            Console.WriteLine();

            var innerJoin2 = from s in studentList // outer sequence
                join st in standardList //inner sequence 
                    on s.StandardID equals st.StandardID // key selector 
                select new
                { // result selector 
                    StudentName = s.StudentName,
                    StandardName = st.StandardName
                };

            foreach (var x1 in innerJoin2)
            {
                Console.WriteLine(
                    $"{nameof(x1.StudentName)}:{x1.StudentName}  {nameof(x1.StandardName)}:{x1.StandardName}");
            }
        }
    }
}
