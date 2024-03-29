using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using AspnetCoreWebApiLab.Controllers.Models;
using AspnetCoreWebApiLab.EntityFramework;
using AspnetCoreWebApiLab.Interfaces;
using AspnetCoreWebApiLab.Interfaces.HttpClients;
using AspnetCoreWebApiLab.Services;
using AspnetCoreWebApiLab.Services.HttpClients;
using AspnetCoreWebApiLab.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace AspnetCoreWebApiLab
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostEnvironment _hostEnvironment;

        /// <summary>
        /// Startup 可注入三个服务，参考 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-5.0
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="hostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _hostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureBaseServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureBaseServices(services);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureBaseServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureBaseServices(services);
        }

        private void ConfigureBaseServices(IServiceCollection services)
        {
            services.AddScoped<IOperationScoped, OperationScoped>();
            services.AddTransient<OperationHandler>();
            services.AddTransient<OperationResponseHandler>();

            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory");
            }).AddTypedClient<GitHubService>().AddHttpMessageHandler<OperationHandler>().AddHttpMessageHandler<OperationResponseHandler>().SetHandlerLifetime(TimeSpan.FromSeconds(1));

            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://github.com/");
            }).AddTypedClient(Refit.RestService.For<IGithubClient>);

            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<TodoContext>(options =>
            {
                options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion);
                options.LogTo(Console.WriteLine);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspnetCoreWebApiLab", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            });

            services.AddAutoMapper(configAction => configAction.AddProfile<AspnetCoreWebApiLabAutoMapperProfile>());
            services.AddSignalR();

            // 允许跨域
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.SetIsOriginAllowed(s => true).WithOrigins("http://127.0.0.1").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddMyServices();

            // 注入配置
            services.Configure<ConfigurationTestModel>(Configuration.GetSection("ConfigurationTest"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            ConfigureBase(app, env, logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            ConfigureBase(app, env, logger);
        }

        private void ConfigureBase(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspnetCoreWebApiLab v1"));
            }

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello");
            //});

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                RequestPath = "/StaticFiles",
                OnPrepareResponse = ctx =>
                {
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append(
                        "Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                RequestPath = "/StaticFiles"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello");
                }).AllowAnonymous();

                //endpoints.MapHealthChecks("/health");

                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });

            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await next.Invoke(); //如果不调用 next.Invoke() 则形成短路请求直接返回
                // Do logging or other work that doesn't write to the Response.
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from 2nd delegate.");
            //});

            logger.LogInformation("Startup finished");
        }
    }
}
