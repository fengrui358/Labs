using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<MiddlewareDelegate> _middlewares = new List<MiddlewareDelegate>();

        public RequestDelegate Build()
        {
            _middlewares.Reverse();
            return httpContext =>
            {
                RequestDelegate next = context =>
                {
                    context.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };

                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }

                return next(httpContext);
            };
        }

        public IApplicationBuilder Use(MiddlewareDelegate middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}