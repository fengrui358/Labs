using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using AutoMapper;
using AutoMapper.Configuration;
using Xunit;

namespace AutoMapperLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<B, A>().ForMember(d=>d.astRiNg, expression => expression.Ignore());
            cfg.CreateMap<A, B>().ForMember(d => d.TestStrNumber, expression => expression.MapFrom(s => s.TesTNumber));
            cfg.CreateMap<A, A>();
            cfg.CreateMap<A, C>().ForPath(d => d.BString, expression => expression.MapFrom(s => s.AString)).ReverseMap();

            var configuration = new MapperConfiguration(cfg);
            var executionPlan = configuration.BuildExecutionPlan(typeof(A), typeof(A));

            var mapper = new Mapper(configuration);

            #region 直接Copy

            var a = new A
            {
                AString = "asfFDSF345FDSfsdf"
            };

            var a2 = mapper.Map<A>(a);
            Assert.NotEqual(a, a2);
            Assert.Equal(a.AString, a2.AString);

            #endregion

            #region 驼峰转换

            var b = new B
            {
                aString = "daf34f"
            };

            var a3 = mapper.Map<A>(b);
            Assert.Equal(b.aString, a3.AString);
            Assert.NotEqual(b.aString, a3.astRiNg);

            a3.AString = "sdafasf";
            var b3 = mapper.Map<B>(a3);
            Assert.Equal(a3.AString, b3.aString);

            #endregion

            #region 名称一样，不同类型映射

            var b4 = new B {TestNumber = "56"};
            var a4 = mapper.Map<A>(b4);
            Assert.Equal(56,a4.TesTNumber);

            #endregion

            #region 枚举和Int转换

            var b5 = new B {Sex = 6};
            var a5 = mapper.Map<A>(b5);
            Assert.Equal(TestSex.Female, a5.Sex);

            var b6 = new B {Sex = 8};
            var a6 = mapper.Map<A>(b6);
            Assert.NotEqual(TestSex.Male, a6.Sex);
            Assert.NotEqual(TestSex.Female, a6.Sex);

            #endregion

            #region 属性名转换

            var a7 = new A {TesTNumber = 435};
            var b7 = mapper.Map<B>(a7);
            Assert.Equal("435", b7.TestStrNumber);

            var a10 = new A {AString = "ddd"};
            var c = mapper.Map<C>(a10);
            Assert.Equal("ddd", c.BString);

            c.AString = null;
            var a11 = mapper.Map<A>(c);
            Assert.Equal("ddd", a11.AString);

            #endregion

            #region 空属性转换

            var a8 = new A
            {
                AString = "ggg",
                astRiNg = "ttt",
                bString = "jjj"
            };

            var a9 = new A
            {
                AString = ""
            };
            mapper.Map(a9, a8);
            Assert.Equal(string.Empty, a8.AString);
            Assert.Equal(null, a8.bString);

            #endregion

            #region 官方Demo

            var flattening = new Flattening();
            flattening.Test();

            var includeMembers = new IncludeMembers();
            includeMembers.Test();

            var reverseMappingAndUnflattening = new ReverseMappingAndUnflattening();
            reverseMappingAndUnflattening.Test();

            var projection = new Projection();
            projection.Test();

            var configurationValidationcs = new ConfigurationValidationcs.ConfigurationValidationcs();
            configurationValidationcs.Test();

            var listsAndArrarys = new ListsAndArrays();
            listsAndArrarys.Test();

            var customTypeConverters = new CustomTypeConverters();
            customTypeConverters.Test();

            #endregion

            Console.WriteLine("Over");
            Console.ReadLine();
        }
    }

    class A
    {
        public string AString { get; set; }
        public string astRiNg { get; set; }

        public string bString { get; set; }

        public int TesTNumber { get; set; }

        public TestSex Sex { get; set; }
    }

    class B
    {
        public string aString { get; set; }

        public string TestNumber { get; set; }

        public int Sex { get; set; }

        public string TestStrNumber { get; set; }
    }

    class C
    {
        public string AString { get; set; }
        public string BString { get; set; }
    }

    enum TestSex
    {
        Male = 4,

        Female = 6
    }

    class BProfile: Profile
    {
        
    }
}
