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
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>());
            var sources = new[]
            {
                new Source { Value = 5 },
                new Source { Value = 6 },
                new Source { Value = 7 }
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
        }

        class Source
        {
            public int Value { get; set; }
        }

        class Destination
        {
            public int Value { get; set; }
        }
    }
}
