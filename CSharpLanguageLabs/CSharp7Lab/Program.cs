using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7Lab
{
    /// <summary>
    /// 展示Visual stuido中C#7.0的新语法 参考：http://www.cnblogs.com/GuZhenYin/p/6526041.html#3638099
    /// https://docs.microsoft.com/zh-cn/dotnet/articles/csharp/csharp-7#generalized-async-return-types
    /// https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region 1. out-variables(Out 变量)

            Console.WriteLine("1. out-variables(Out 变量)");
            StringOut(out string innerOutVariable);
            Console.WriteLine("输出变量：" + innerOutVariable);
            Console.WriteLine();

            #endregion

            #region 2. Tuples(元组)

            Console.WriteLine("2. Tuples(元组) 需要安装：System.ValueTuple");
            (var name, _) = GetPersonInfo(); //不关心年龄参数
            Console.WriteLine($"Name:{name}");
            Console.WriteLine();

            #endregion

            #region 3. PatternMatching(匹配模式)

            Console.WriteLine("3. PatternMatching(匹配模式)");
            object a = 1;
            if (a is int b)
            {
                Console.WriteLine("模式匹配判断是整形，数值为：" + b);
            }

            if (!(a is double c))
            {
                Console.WriteLine("模式匹配判断不是浮点型");
            }

            //Switch 判断
            var switchResult = Add(a);
            Console.WriteLine("Switch模式匹配:" + switchResult);

            Console.WriteLine();

            #endregion

            #region 4. ref locals and returns(局部变量和引用返回)

            Console.WriteLine("4. ref locals and returns(局部变量和引用返回)");
            int x = 3;
            ref int x1 = ref x;  //注意这里,我们通过ref关键字 把x赋给了x1
            x1 = 2;
            Console.WriteLine($"改变后的变量 {nameof(x)} 值为: {x}");

            #endregion

            #region 5. Local Functions (局部函数)

            Console.WriteLine("5. Local Functions (局部函数)");
            int data = Dosomeing(100, 200);
            Console.WriteLine(data);

            int Dosomeing(int d1, int d2) => d1 + d2;

            Console.WriteLine();

            #endregion

            #region 6. More expression-bodied members(更多的函数成员的表达式体)

            Console.WriteLine("6. More expression-bodied members(更多的函数成员的表达式体)");
            Console.WriteLine();

            #endregion

            #region 7. throw Expressions (异常表达式)

            Console.WriteLine("7. throw Expressions (异常表达式)");
            try
            {
                string input = null;
                IsNull(input);

                string IsNull(string i) => i ?? throw new ArgumentNullException(nameof(i));
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine("捕获到期望异常");
            }
            Console.WriteLine();

            #endregion

            #region 8. Generalized async return types (通用异步返回类型)

            Console.WriteLine("8. Generalized async return types (通用异步返回类型)");

            Action act = async () =>
            {
                var ccc = new CaCheContext();
                int data2 = await ccc.CachedFunc();
                Console.WriteLine(data2);
                int data3 = await ccc.CachedFunc();
                Console.WriteLine(data3);
            };
            // 调用委托  
            act();

            System.Threading.Thread.Sleep(5010);
            Console.WriteLine();

            #endregion

            #region 9. Numeric literal syntax improvements(数值文字语法改进)

            Console.WriteLine("9. Numeric literal syntax improvements(数值文字语法改进)");

            int aa = 123_456;
            int bb = 0xAB_CD_EF;
            int cc = 123456;
            int dd = 0xABCDEF;
            Console.WriteLine(aa == cc);
            Console.WriteLine(bb == dd);

            //如上代码会显示两个true,在数字中用"_"分隔符不会影响结果,只是为了提高可读性

            #endregion

            Console.ReadLine();
        }

        /// <summary>
        /// 1. out-variables(Out 变量)
        /// </summary>
        /// <param name="o"></param>
        private static void StringOut(out string o)
        {
            o = "test-out";
        }

        /// <summary>
        /// 2. Tuples(元组) 需要安装：System.ValueTuple
        /// </summary>
        /// <returns></returns>
        private static (string name, int age) GetPersonInfo()
        {
            return ("free", 30);
        }

        /// <summary>
        /// 3. PatternMatching(匹配模式)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static dynamic Add(object a)
        {
            dynamic data;
            switch (a)
            { 
                case int b when b < 0:
                    data = b++;
                    break;
                case int b:
                    data = b--;
                    break;
                case string c:
                    data = c + "aaa";
                    break;
                default:
                    data = null;
                    break;
            }

            return data;
        }
    }

    /// <summary>
    /// 6. More expression-bodied members(更多的函数成员的表达式体)
    /// </summary>
    class TestClass
    {
        private string _label;

        public string Label
        {
            get => _label;
            set => _label = value ?? string.Empty;
        }

        public TestClass(string label) => _label = label;
    }

    public class CaCheContext
    {
        public ValueTask<int> CachedFunc()
        {
            return (cache) ? new ValueTask<int>(cacheResult) : new ValueTask<int>(loadCache());
        }

        private bool cache = false;
        private int cacheResult;
        private async Task<int> loadCache()
        {
            // simulate async work:
            await Task.Delay(5000);
            cache = true;
            cacheResult = 100;
            return cacheResult;
        }
    }
}
