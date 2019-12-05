using System;
using AutoMapper;
using Xunit;

namespace AutoMapperLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, A>();
            }));

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



            #endregion

            #region 属性名转换



            #endregion

            #region 枚举和Int转换

            

            #endregion

            Console.ReadLine();
        }
    }

    class A
    {
        public string AString { get; set; }
    }

    class B
    {
        public string aString { get; set; }
    }


}
