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
    /// 对TinyMapper和AutoMapper进行性能测试，结果是AutoMapper的性能更胜一筹，不像TinyMapper宣传的那样，而且AutoMapper的灵活性也更高
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //构造测试数据
            Console.WriteLine("构造测试数据");
            var testDatas = new List<StubClass>();

            for (int i = 0; i < 100000; i++)
            {
                var data = new StubClass
                {
                    StringA = Guid.NewGuid().ToString(),
                    IntB = i,
                    StringListC = new List<StubSubClass>()
                };
                for (int j = 0; j < 100; j++)
                {
                    data.StringListC.Add(new StubSubClass { StringA = Guid.NewGuid().ToString(), IntB = i});
                }

                testDatas.Add(data);
            }

            #region TinyMapper

            Console.WriteLine("测试TinyMapper");

            var stopWatch = Stopwatch.StartNew();

            TinyMapper.Map<List<StubClass>>(testDatas);

            stopWatch.Stop();
            Console.WriteLine("TinyMapper耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region AutoMapper

            Mapper.Initialize(new MapperConfigurationExpression());

            Console.WriteLine("测试AutoMapper");

            stopWatch.Restart();

            Mapper.Map<List<StubClass>>(testDatas);

            stopWatch.Stop();
            Console.WriteLine("AutoMapper耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            Console.Read();
        }
    }
}
