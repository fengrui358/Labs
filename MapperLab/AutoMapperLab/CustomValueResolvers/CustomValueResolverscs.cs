using AutoMapper;
using Xunit;

namespace AutoMapperLab.CustomValueResolvers
{
    class CustomValueResolverscs
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Source, Destination>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom<CustomResolver>()));
            configuration.AssertConfigurationIsValid();

            var source = new Source
            {
                Value1 = 5,
                Value2 = 7
            };

            var mapper = new Mapper(configuration);
            var result = mapper.Map<Source, Destination>(source);

            Assert.Equal(12, result.Total);
        }

        class Source
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
        }

        class Destination
        {
            public int Total { get; set; }
        }

        class CustomResolver : IValueResolver<Source, Destination, int>
        {
            public int Resolve(Source source, Destination destination, int member, ResolutionContext context)
            {
                return source.Value1 + source.Value2;
            }
        }
    }
}
