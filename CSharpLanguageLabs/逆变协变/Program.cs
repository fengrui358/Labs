using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLanguageLabs
{
    /// <summary>
    /// 参考文章：http://www.cnblogs.com/zhaopei/p/5814610.html
    /// </summary>
    class Program
    {
        delegate T A1<out T>();

        delegate void B1<in T>(T input);

        static void Main(string[] args)
        {
            //out关键字为协变，协变用于输出的泛型变量，可自动转换为基类
            A1<string> a1 = () => "测试输出";
            A1<object> a2 = a1;

            //in关键字为逆变，逆变用于输入参数的泛型变量，由基类变为子类这样的反向变化
            B1<object> b1 = input => {};
            B1<string> b2 = b1;

            //所谓的逆变其实只是编译后进行了强制类型转换而已
        }
    }
}
