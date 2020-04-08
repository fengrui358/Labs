using System;
using System.Threading.Tasks;

namespace AspNetCoreMini
{
    /// <summary>
    /// https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html
    /// 200行代码，7个对象——让你了解ASP.NET Core框架的本质
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            await new WebHostBuilder()
                .UseHttpListener("http://127.0.0.1:5678/", "http://10.15.4.41:7896/")
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware))
                .Build()
                .StartAsync();
        }

        public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        public static RequestDelegate BazMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Baz");
            await next(context);
        };
    }
}