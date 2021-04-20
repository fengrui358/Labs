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
    }
}
