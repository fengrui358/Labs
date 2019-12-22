using System;
using AutoMapper;
using Xunit;

// ReSharper disable once CheckNamespace
namespace AutoMapperLab.ConfigurationValidationcs
{
    class ConfigurationValidationcs
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Source, Destination>());

            //下面这一句由于目标类型中属性不能够全部被映射校验会报错
            //Assert.Throws(typeof(AutoMapperConfigurationException), () => configuration.AssertConfigurationIsValid());
        }
    }

    public class Source
    {
        public int SomeValue { get; set; }
    }

    public class Destination
    {
        public int SomeValuefff { get; set; }
    }
}
