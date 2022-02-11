// ╡н©╪ндуб
// https://mp.weixin.qq.com/s/bO7QpH0mvaW6i192fu_SCg
// https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
// https://www.cnblogs.com/sheng-jie/p/how-much-you-know-about-diagnostic-in-dotnet.html

using System.Diagnostics;
using System.Net;
using DiagnosticsSource;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel((context, options) =>
{
    // options.Listen(IPAddress.Any, 5001, listenOptions => listenOptions.Protocols = HttpProtocols.Http3);
});
var app = builder.Build();

var listenter = app.Services.GetRequiredService<DiagnosticListener>();
listenter.Subscribe(new DiagnosticObserver());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
