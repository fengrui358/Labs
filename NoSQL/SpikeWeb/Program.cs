using SpikeLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var spikeInMemery = new SpikeInMemery();
spikeInMemery.InitStock(100000).Wait();

var spikeInRedis = new SpikeInRedis();
spikeInRedis.InitStock(100000).Wait();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/spikeInMemery/{userId}", async (int userId) =>
{
    var result = await spikeInMemery.Decrease(userId);
    return result;
});

app.MapGet("/spikeInRedis/{userId}", async (int userId) =>
{
    var result = await spikeInRedis.Decrease(userId);
    return result;
});

app.Run("https://localhost:7009");