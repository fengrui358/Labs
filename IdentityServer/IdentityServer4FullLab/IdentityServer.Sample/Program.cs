using IdentityServer.Sample;

var builder = WebApplication.CreateBuilder(args);

// 配置 IdentityServer
builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential() // 添加开发者证书
        .AddInMemoryApiResources(Config.GetApis())
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddInMemoryClients(Config.GetClients());

var app = builder.Build();

app.UseIdentityServer();

app.Run(ServiceConfigs.ServiceConfigs.IdentityServerUrl);
