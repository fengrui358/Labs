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
using AspnetCoreWebApiLab.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreWebApiLab
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            // ÔÊÐí¿çÓò
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.SetIsOriginAllowed(s => true).WithOrigins("http://127.0.0.1").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspnetCoreWebApiLab v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello");
                }).AllowAnonymous();

                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
