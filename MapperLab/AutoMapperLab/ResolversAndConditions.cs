using AutoMapper;
using AutoMapper.Configuration;

namespace AutoMapperLab
{
    class ResolversAndConditions
    {
        public void Test()
        {
            var configure = new MapperConfigurationExpression();
            configure.CreateMap<SourceClass, TargetClass>()
                .ForMember(d => d.ValueLength, o => o.MapFrom(s => s.Value.Length + 5))
                //.ForMember(d => d.ValueLength, o => o.MapFrom(s => s != null ? s.Value.Length : 0))
                .ForAllMembers(o => o.Condition((src, dest, value) => value != null)); //通过测试不需要加空值判断
            
            var mapperConfiguration = new MapperConfiguration(configure);
            var express = mapperConfiguration.BuildExecutionPlan(typeof(SourceClass), typeof(TargetClass));
            
            var source = new SourceClass { Value = null };
            var target = new TargetClass();

            var mapper = new Mapper(mapperConfiguration);
            mapper.Map(source, target);
        }

        public class SourceClass
        {
            public string Value { get; set; }
        }

        public class TargetClass
        {
            public int ValueLength { get; set; }
        }
    }
}
