using System;
using System.Collections.Generic;
using System.Linq;
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
            cfg.CreateMap<B, A>();
            cfg.CreateMap<A, B>();
            cfg.CreateMap<A, A>();

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
            Assert.Equal(b.aString, a3.astRiNg);

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



            #endregion

            #region 属性名转换



            #endregion

            #region 官方Demo

            var flattening = new Flattening();
            flattening.Test();

            #endregion

            Console.ReadLine();
        }
    }

    class A
    {
        public string AString { get; set; }
        public string astRiNg { get; set; }

        public int TesTNumber { get; set; }
    }

    class B
    {
        public string aString { get; set; }

        public string TestNumber { get; set; }
    }

    class BProfile: Profile
    {
        
    }
}
