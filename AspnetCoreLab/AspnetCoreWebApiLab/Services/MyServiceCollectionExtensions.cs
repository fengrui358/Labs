using AspnetCoreWebApiLab.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetCoreWebApiLab.Services
{
    /// <summary>
    /// 应该按注册的服务类型划分
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
    /// </summary>
    public static class MyServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IMyService, MyService>();
            return services;
        }
    }
}
