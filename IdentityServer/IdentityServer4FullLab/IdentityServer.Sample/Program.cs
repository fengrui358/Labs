using IdentityServer.Sample;

var builder = WebApplication.CreateBuilder(args);

// ���� IdentityServer
builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential() // ��ӿ�����֤��
        .AddInMemoryApiResources(Config.GetApis())
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddInMemoryClients(Config.GetClients());

var app = builder.Build();

app.UseIdentityServer();

app.Run(ServiceConfigs.ServiceConfigs.IdentityServerUrl);
