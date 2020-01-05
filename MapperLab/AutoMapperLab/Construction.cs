using AutoMapper;
using Xunit;

namespace AutoMapperLab
{
    class Construction
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Source, SourceDto>());
            var mapper = new Mapper(configuration);

            var source = new Source {Value = 5};
            var dest = mapper.Map<SourceDto>(source);

            Assert.Equal(5, dest.Value);


            var configuration2 = new MapperConfiguration(cfg =>
                cfg.CreateMap<Source2, SourceDto2>()
                    .ForCtorParam("valueParamSomeOtherName", opt => opt.MapFrom(src => src.Value))
            );
            
            var mapper2 = new Mapper(configuration2);
            var source2 = new Source2 {Value = 6};
            var dest2 = mapper2.Map<SourceDto2>(source2);
            Assert.Equal(6, dest2.Value);

            //禁用构造函数注入
            //var configuration = new MapperConfiguration(cfg => cfg.DisableConstructorMapping());

            //禁用私有构造函数
            // don't map private constructors
            //var configuration = new MapperConfiguration(cfg => cfg.ShouldUseConstructor = ci => !ci.IsPrivate);
        }

        class Source
        {
            public int Value { get; set; }
        }
        class SourceDto
        {
            public SourceDto(int value)
            {
                _value = value;
            }

            private int _value;
            public int Value => _value;
        }

        public class Source2
        {
            public int Value { get; set; }
        }
        public class SourceDto2
        {
            public SourceDto2(int valueParamSomeOtherName)
            {
                _value = valueParamSomeOtherName;
            }
            private int _value;
            public int Value => _value;
        }
    }
}
