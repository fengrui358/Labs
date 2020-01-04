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

        public void Test2()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Source, Destination>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(new MultBy2Resolver())));
            configuration.AssertConfigurationIsValid();

            var source = new Source
            {
                Value1 = 5,
                Value2 = 7
            };

            var mapper = new Mapper(configuration);

            //只关心目标属性，不在乎类型，设置源和目标类型为object
            var result = mapper.Map<Source, Destination>(source);

            Assert.Equal(30, result.Total);
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

        /// <summary>
        /// 只关心目标属性，不在乎类型，设置源和目标类型为object
        /// </summary>
        public class MultBy2Resolver : IValueResolver<object, object, int>
        {
            public int Resolve(object source, object dest, int destMember, ResolutionContext context)
            {
                return (destMember + 15) * 2;
            }
        }
    }
}
