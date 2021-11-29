// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;

Console.WriteLine("Console client start");

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync($"{ServiceConfigs.ServiceConfigs.IdentityServerUrl}");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    Console.ReadKey();
    return;
}

// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.ReadKey();
    return;
}
else
{
    Console.WriteLine(tokenResponse.Json);
}

Console.ReadKey();
