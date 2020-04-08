namespace AspNetCoreMini
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(MiddlewareDelegate middleware);
        RequestDelegate Build();
    }
}