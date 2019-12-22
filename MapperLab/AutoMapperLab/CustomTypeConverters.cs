using System;
using System.Reflection;
using AutoMapper;
using Xunit;

namespace AutoMapperLab
{
    class CustomTypeConverters
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg => {
                //cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                //cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());  //测试发现字符串转整型和时间都是默认转换
                cfg.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
                cfg.CreateMap<Source, Destination>();
            });
            configuration.AssertConfigurationIsValid();

            var mapper = new Mapper(configuration);

            var source = new Source
            {
                Value1 = "5",
                Value2 = "01/01/2000",
                Value3 = "AutoMapperLab.CustomTypeConverters+Destination"
            };

            Destination result = mapper.Map<Source, Destination>(source);
            Assert.Equal(typeof(Destination), result.Value3);
        }

        class Source
        {
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
        }

        class Destination
        {
            public int Value1 { get; set; }
            public DateTime Value2 { get; set; }
            public Type Value3 { get; set; }
        }

        class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return System.Convert.ToDateTime(source);
            }
        }

        class TypeTypeConverter : ITypeConverter<string, Type>
        {
            public Type Convert(string source, Type destination, ResolutionContext context)
            {
                return Assembly.GetExecutingAssembly().GetType(source);
            }
        }
    }
}
