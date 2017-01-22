using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EqualLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //equeal
            var a = new LabClass(1);
            var b = new LabClass(1);

            //重载了LabClass的Equals方法
            Console.WriteLine("重载了LabClass的Equals方法：" + a.Equals(b));

            //重载了LabClass的==和!=运算符
            Console.WriteLine("重载了LabClass的==和!=运算符：" + (a == b));

            //直接判断引用相等
            Console.WriteLine("直接判断引用相等：" + object.ReferenceEquals(a, b));

            //重载了重载了LabClass的GetHashCode
            var hashSet = new HashSet<LabClass>();
            hashSet.Add(a);

            Console.WriteLine("重载了重载了LabClass的GetHashCode：" + hashSet.Contains(b));

            Console.Read();
        }
    }
}
