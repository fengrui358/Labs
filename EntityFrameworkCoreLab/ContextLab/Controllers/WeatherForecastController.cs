using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ContextLab.DbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var blogs = await _dbContext.Blogs.ToListAsync();
            Console.WriteLine(JsonConvert.SerializeObject(blogs));

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
