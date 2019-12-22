using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Xunit;

namespace AutoMapperLab
{
    /// <summary>
    /// http://docs.automapper.org/en/stable/Lists-and-arrays.html
    /// </summary>
    class ListsAndArrays
    {
        public void Test()
        {
            var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Source, Destination>();
                    cfg.CreateMap<ParentSource, ParentDestination>().Include<ChildSource, ChildDestination>();
                    cfg.CreateMap<ChildSource, ChildDestination>();
                }
            );
            var sources = new[]
            {
                new Source {Value = 5},
                new Source {Value = 6},
                new Source {Value = 7}
            };

            var mapper = new Mapper(configuration);

            IEnumerable<Destination> ienumerableDest = mapper.Map<Source[], IEnumerable<Destination>>(sources);
            ICollection<Destination> icollectionDest = mapper.Map<Source[], ICollection<Destination>>(sources);
            IList<Destination> ilistDest = mapper.Map<Source[], IList<Destination>>(sources);
            List<Destination> listDest = mapper.Map<Source[], List<Destination>>(sources);
            Destination[] arrayDest = mapper.Map<Source[], Destination[]>(sources);

            for (var i = 0; i < sources.Length; i++)
            {
                Assert.NotEqual(ilistDest[i].GetHashCode(), sources[i].GetHashCode());
            }

            var sources2 = new[]
            {
                new ParentSource(),
                new ChildSource(),
                new ParentSource()
            };

            var destinations = mapper.Map<ParentSource[], ParentDestination[]>(sources2);

            Assert.IsType<ParentDestination>(destinations[0]);
            Assert.IsType<ChildDestination>(destinations[1]);
            Assert.IsType<ParentDestination>(destinations[2]);
        }

        class Source
        {
            public int Value { get; set; }
        }

        class Destination
        {
            public int Value { get; set; }
        }

        class ParentSource
        {
            public int Value1 { get; set; }
        }

        class ChildSource : ParentSource
        {
            public int Value2 { get; set; }
        }

        class ParentDestination
        {
            public int Value1 { get; set; }
        }

        class ChildDestination : ParentDestination
        {
            public int Value2 { get; set; }
        }
    }
}