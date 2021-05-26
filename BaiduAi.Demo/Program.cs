using System;
using BaiduAi.Demo.APIs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BaiduAi.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ApiConfig>(hostContext.Configuration.GetSection("Baidu"));
                    services.AddSingleton<Orc>();
                    services.AddSingleton<FaceRecognition>();
                    services.AddSingleton<BodyAnalysis>();
                    services.AddSingleton<Speech>();

                    services.AddHostedService<BaiduAi>();
                });
    }
}
