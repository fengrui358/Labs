using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace MapperLab
{
    /// <summary>
    /// 对TinyMapper和AutoMapper进行性能测试，结果相差不大
    /// 注：TinyMapper对Model含Dictinory时Bind不能处理
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //构造测试数据
            Console.WriteLine("构造测试数据");
            var testDatas = new List<StubClass>();

            for (int i = 0; i < 30; i++)
            {
                var data = new StubClass
                {
                    StringA = Guid.NewGuid().ToString(),
                    IntB = i,
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "support@tinymapper.net",
                    Address = "Wall Street",
                    CreateTime = DateTime.Now,
                    Nickname = "Object Mapper",
                    Phone = "Call Me Maybe ",
                    StubSubClass = new StubSubClass
                    {
                        StringA = Guid.NewGuid().ToString(),
                        IntB = i,
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "support@tinymapper.net",
                        Address = "Wall Street",
                        CreateTime = DateTime.Now,
                        Nickname = "Object Mapper",
                        Phone = "Call Me Maybe "
                    },
                    StringListC = new List<StubSubClass>(),
                    Dictionary = new Dictionary<string, string>()
                };

                for (int j = 0; j < 100; j++)
                {
                    data.Dictionary.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                }

                for (int j = 0; j < 1000; j++)
                {
                    data.StringListC.Add(new StubSubClass
                    {
                        StringA = Guid.NewGuid().ToString(),
                        IntB = i,
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "support@tinymapper.net",
                        Address = "Wall Street",
                        CreateTime = DateTime.Now,
                        Nickname = "Object Mapper",
                        Phone = "Call Me Maybe "
                    });
                }

                testDatas.Add(data);
            }

            #region TinyMapper

            Console.WriteLine("测试TinyMapper");

            var stopWatch = Stopwatch.StartNew();

            //如果实体含有Dictinory则Bind会报错？
            //TinyMapper.Bind<StubClass, StubClass>();
            //TinyMapper.Bind<StubSubClass, StubSubClass>();

            var outs = TinyMapper.Map<List<StubClass>>(testDatas);
            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试TinyMapper：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("TinyMapper耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region Clean

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

            #region Clean

            outs = null;
            GC.Collect();

            #endregion

            #region 二进制序列化

            Console.WriteLine("测试二进制序列化");

            stopWatch.Restart();

            var b = new BinaryFormatter();
            var stream = new MemoryStream();

            b.Serialize(stream, testDatas);
            stream.Position = 0;

            outs = b.Deserialize(stream) as List<StubClass>;
            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试二进制序列化：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("二进制序列化耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region Clean

            outs = null;
            GC.Collect();

            #endregion

            #region Json序列化

            Console.WriteLine("测试Json序列化");

            stopWatch.Restart();

            var json = JsonConvert.SerializeObject(testDatas);
            outs = JsonConvert.DeserializeObject<List<StubClass>>(json);

            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试Json序列化：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("Json序列化耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region Clean

            outs = null;
            GC.Collect();

            #endregion

            #region 手动拷贝

            Console.WriteLine("测试手动拷贝");

            stopWatch.Restart();

            outs = ManualCopy(testDatas);

            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试手动拷贝：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("手动拷贝耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region Clean

            outs = null;
            GC.Collect();

            #endregion

            #region MemberwiseClone

            Console.WriteLine("测试MemberwiseClone");

            stopWatch.Restart();

            outs = MemberwiseClone(testDatas);

            if (!VerifyReferenceIsDeepCopy(testDatas, outs))
            {
                Console.WriteLine("测试MemberwiseClone：映射出错");
            }

            stopWatch.Stop();
            Console.WriteLine("MemberwiseClone耗时：" + stopWatch.ElapsedMilliseconds);

            #endregion

            #region Clean

            outs = null;
            GC.Collect();

            #endregion

            Console.Read();
        }

        private static bool VerifyReferenceIsDeepCopy(List<StubClass> a, List<StubClass> b)
        {
            //比较两个集合是完全进行的深度拷贝
            if (a == b || a.Last() == b.Last() || a.Last().StringListC == b.Last().StringListC ||
                a.Last().StringA != b.Last().StringA || a.Last().StringListC.Last().StringA !=
                b.Last().StringListC.Last().StringA)
            {
                return false;
            }

            return true;
        }

        private static List<StubClass> ManualCopy(List<StubClass> source)
        {
            var result = new List<StubClass>();
            foreach (var stubClass in source)
            {
                var data = new StubClass
                {
                    StringA = stubClass.StringA,
                    IntB = stubClass.IntB,
                    Id = stubClass.Id,
                    FirstName = stubClass.FirstName,
                    LastName = stubClass.LastName,
                    Email = stubClass.Email,
                    Address = stubClass.Address,
                    CreateTime = stubClass.CreateTime,
                    Nickname = stubClass.Nickname,
                    Phone = stubClass.Phone,
                    StringListC = new List<StubSubClass>(),
                    Dictionary = new Dictionary<string, string>()
                };

                var stubSubClass = new StubSubClass
                {
                    StringA = stubClass.StubSubClass.StringA,
                    IntB = stubClass.StubSubClass.IntB,
                    Id = stubClass.StubSubClass.Id,
                    FirstName = stubClass.StubSubClass.FirstName,
                    LastName = stubClass.StubSubClass.LastName,
                    Email = stubClass.StubSubClass.Email,
                    Address = stubClass.StubSubClass.Address,
                    CreateTime = stubClass.StubSubClass.CreateTime,
                    Nickname = stubClass.StubSubClass.Nickname,
                    Phone = stubClass.StubSubClass.Phone
                };
                data.StubSubClass = stubSubClass;

                foreach (KeyValuePair<string, string> keyValuePair in stubClass.Dictionary)
                {
                    data.Dictionary.Add(keyValuePair.Key, keyValuePair.Value);
                }

                for (int j = 0; j < stubClass.StringListC.Count; j++)
                {
                    data.StringListC.Add(new StubSubClass
                    {
                        StringA = stubClass.StringListC[j].StringA,
                        IntB = stubClass.StringListC[j].IntB,
                        Id = stubClass.StringListC[j].Id,
                        FirstName = stubClass.StringListC[j].FirstName,
                        LastName = stubClass.StringListC[j].LastName,
                        Email = stubClass.StringListC[j].Email,
                        Address = stubClass.StringListC[j].Address,
                        CreateTime = stubClass.StringListC[j].CreateTime,
                        Nickname = stubClass.StringListC[j].Nickname,
                        Phone = stubClass.StringListC[j].Phone
                    });
                }

                result.Add(data);
            }

            return result;
        }

        private static List<StubClass> MemberwiseClone(List<StubClass> source)
        {
            var result = new List<StubClass>();
            foreach (var stubClass in source)
            {
                result.Add(stubClass.Copy());
            }

            return result;
        }
    }
}