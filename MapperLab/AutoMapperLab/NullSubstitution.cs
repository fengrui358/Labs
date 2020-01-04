using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Xunit;

namespace AutoMapperLab
{
    class NullSubstitution
    {
        public void Test()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SourceClass, TargetClass>()
                .ForMember(destination => destination.Value, opt => opt.NullSubstitute("Other Value")));

            var source = new SourceClass { Value = null };
            var mapper = config.CreateMapper();
            var dest = mapper.Map<SourceClass, TargetClass>(source);

            Assert.Equal("Other Value", dest.Value);

            source.Value = "Not null";

            dest = mapper.Map<SourceClass, TargetClass>(source);

            Assert.Equal("Not null", dest.Value);
        }

        class SourceClass
        {
            public string Value { get; set; }
        }

        class TargetClass
        {
            public string Value { get; set; }
        }
    }
}
