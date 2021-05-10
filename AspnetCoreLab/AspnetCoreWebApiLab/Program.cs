using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCoreWebApiLab
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var config = host.Services.GetRequiredService<IConfiguration>();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            foreach (var c in config.AsEnumerable())
            {
                logger.LogInformation("{key}={value}", c.Key, c.Value);
            }

#if DEBUG
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
#endif
            var hostEnvironment = host.Services.GetService<IHostEnvironment>();
            logger.LogInformation("The host environment is {hostEnvironment}", hostEnvironment?.EnvironmentName);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostBuilderContext, config) =>
                {
                    var switchMappings = new Dictionary<string, string>
                    {
                        { "-k1", "key1" },
                        { "-k2", "key2" },
                        { "--alt3", "key3" },
                        { "--alt4", "key4" },
                        { "--alt5", "key5" },
                        { "--alt6", "key6" },
                    };

                    config.AddCommandLine(args, switchMappings);

                    // µ¥ÎÄ¼þÅäÖÃ
                    //config.AddKeyPerFile()
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
