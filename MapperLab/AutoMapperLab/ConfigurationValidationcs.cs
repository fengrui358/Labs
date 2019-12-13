using AutoMapper;

// ReSharper disable once CheckNamespace
namespace AutoMapperLab.ConfigurationValidationcs
{
    class ConfigurationValidationcs
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Source, Destination>());

            configuration.AssertConfigurationIsValid();
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
