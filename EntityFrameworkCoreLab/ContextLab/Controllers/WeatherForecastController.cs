using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ContextLab.DbContext;
using ContextLab.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContextLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DefaultDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DefaultDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //EFCore p305
            //var blogsEagerLoading = await _dbContext.Blogs.Include(s => s.Posts).Include(s => s.Author).ToListAsync();

            //EFCore p310
            //var blogsExplicitLoading = await _dbContext.Blogs.SingleOrDefaultAsync(s => s.BlogId == 1);
            //await _dbContext.Entry(blogsExplicitLoading).Collection(s => s.Posts).LoadAsync();
            //await _dbContext.Entry(blogsExplicitLoading).Reference(s => s.Author).LoadAsync();
            
            //EFCore p312 Lazy Loading
            var blogsLazyLoading = await _dbContext.Blogs.ToListAsync();
            
            //EFCore p316 Serialization
            //var blogsJson = JsonSerializer.Serialize(blogsLazyLoading);

            var blogs = await _dbContext.Blogs.Where(s=>s.Url.Contains("manual", StringComparison.Ordinal)).ToListAsync();
            Console.WriteLine(JsonSerializer.Serialize(blogs));

            await _dbContext.AddRangeAsync(new List<Other>
            {
                new() {Id = Guid.NewGuid(), TestString = "Insert_1"},
                new() {Id = Guid.NewGuid(), TestString = "Insert_2"}
            });
            await _dbContext.AddAsync(new Other {Id = Guid.NewGuid(), TestString = "Insert"});
            await _dbContext.SaveChangesAsync();

            await _dbContext.AddAsync(new RssBlog {AuthorId = 1, Url = $"ManualAddBlog_{Guid.NewGuid():N}", RssUrl = "ManualAddRssUrl"});
            await _dbContext.SaveChangesAsync();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// EFCore P367 （在mysql中没有出现并发问题）
        /// </summary>
        /// <returns></returns>
        [Route(nameof(ConcurrencyTest))]
        [HttpGet]
        public async Task<int> ConcurrencyTest()
        {
            var blog = await _dbContext.Blogs.SingleOrDefaultAsync(s => s.BlogId == 1);
            blog.Url = Guid.NewGuid().ToString();

            // change data simulate a concurrency conflict
            await _dbContext.Database.ExecuteSqlRawAsync($"update blogs set blogs.Url = '{Guid.NewGuid()}' where blogs.BlogId = 1;");

            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    var result = _dbContext.SaveChanges();
                    saved = true;

                    return result;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    foreach (var entry in e.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                    }
                }
            }

            return 0;
        }
    }
}
