// https://mp.weixin.qq.com/s/YG9ZAnAbAAgAZDWBBOmoXw

var builder = WebApplication.CreateBuilder(args);
// 不要忘了加这两个服务
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

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
    return $"你提交的内容：{name} - {age}";
});

app.MapGet("/", () => "Hello World!");

app.Run();
