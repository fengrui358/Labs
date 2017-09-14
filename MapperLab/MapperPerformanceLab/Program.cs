using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Nelibur.ObjectMapper;

namespace MapperLab
{
    /// <summary>
    /// 对TinyMapper和AutoMapper进行性能测试，结果相差不大
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //构造测试数据
            Console.WriteLine("构造测试数据");
            var testDatas = new List<StubClass>();

            for (int i = 0; i < 10000; i++)
            {
                var data = new StubClass
                {
                    StringA = Guid.NewGuid().ToString(),
                    IntB = i,
                    StringListC = new List<StubSubClass>()
                };
                for (int j = 0; j < 1000; j++)
                {
                    data.StringListC.Add(new StubSubClass { StringA = Guid.NewGuid().ToString(), IntB = i});
                }

                testDatas.Add(data);
            }

            #region TinyMapper

            Console.WriteLine("测试TinyMapper");

            var stopWatch = Stopwatch.StartNew();

            var outs = TinyMapper.Map<List<StubClass>>(testDatas);
            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试TinyMapper：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("TinyMapper耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region 清理

            outs = null;
            GC.Collect();

            #endregion

            #region AutoMapper

            Console.WriteLine("测试AutoMapper");

            stopWatch.Restart();

            Mapper.Initialize(s =>
            {
                s.CreateMissingTypeMaps = true;
                s.CreateMap<StubClass, StubClass>();
                s.CreateMap<StubSubClass, StubSubClass>();
            });

            outs = Mapper.Map<List<StubClass>>(testDatas);
            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试AutoMapper：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("AutoMapper耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            Console.Read();
        }

        private static bool VerifyReferenceIsDeepCopy(List<StubClass> a, List<StubClass> b)
        {
            //比较两个集合是完全进行的深度拷贝
            if (a == b || a.First() == b.First() || a.First().StringListC == b.First().StringListC ||
                a.First().StringA != b.First().StringA || a.First().StringListC.First().StringA !=
                b.First().StringListC.First().StringA)
            {
                return false;
            }

            return true;
        }
    }
}
