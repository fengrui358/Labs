// https://mp.weixin.qq.com/s/YG9ZAnAbAAgAZDWBBOmoXw

using DotNet6MiniWebApi;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// ��Ҫ���˼�����������
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IComputer, ComputerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/greet", () =>
{
    return "What the fuck?";
});

app.MapPost("/submit", (string name, int age) =>
{
    return $"���ύ�����ݣ�{name} - {age}";
});

app.MapGet("/md5", ([FromQuery(Name = "msg")] string data) =>
{
    byte[] buffer = Encoding.UTF8.GetBytes(data);
    using MD5 md5ec = MD5.Create();
    byte[] comres = md5ec.ComputeHash(buffer);
    return $"���ܽ����{Convert.ToHexString(comres).ToLower()}";
});

app.MapPost("/comp", (int n1, int n2, int n3, IComputer compsv) =>
{
    int r = compsv.RunIt(n1, n2, n3);
    return $"��������{r}";
});

app.MapGet("/", () => "Hello World!");

app.Run();
