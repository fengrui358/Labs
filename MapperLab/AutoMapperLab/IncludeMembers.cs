using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using Xunit;

namespace AutoMapperLab
{
    class IncludeMembers
    {
        public void Test()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<Source, Destination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource).ReverseMap();
            cfg.CreateMap<InnerSource, Destination>(MemberList.None);
            cfg.CreateMap<OtherInnerSource, Destination>(MemberList.None);
            cfg.CreateMap<Destination, InnerSource>(MemberList.None);
            cfg.CreateMap<Destination, OtherInnerSource>(MemberList.None);

            var source = new Source
            {
                Name = "name",
                InnerSource = new InnerSource { Name = "inner name", Description = "description" },
                OtherInnerSource = new OtherInnerSource { Name = "other name", Description = "other description", Title = "title" }
            };

            var configuration = new MapperConfiguration(cfg);
            var plan = configuration.BuildExecutionPlan(typeof(Source), typeof(Destination));
            var mapper = new Mapper(configuration);

            var destination = mapper.Map<Destination>(source);

            Assert.Equal("name", destination.Name);
            Assert.Equal("description", destination.Description);
            Assert.Equal("title", destination.Title);

            var source2 = mapper.Map<Source>(destination);
            Assert.Equal("name", source2.Name);
            Assert.Equal("description", source2.InnerSource.Description);
            Assert.Equal("title", source2.OtherInnerSource.Title);

        }
    }

    class Source
    {
        public string Name { get; set; }
        public InnerSource InnerSource { get; set; }
        public OtherInnerSource OtherInnerSource { get; set; }
    }
    class InnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class OtherInnerSource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
    class Destination
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Test { get; set; }
    }
}
