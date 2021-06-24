using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TestToken("");
            var token = await GetAsync();
            await TestToken(token);

            Console.ReadLine();
        }

        private static async Task<string> GetAsync()
        {
            using (var httpClient = new HttpClient{BaseAddress = new Uri("http://localhost:44362") })
            {
                var document = await httpClient.GetDiscoveryDocumentAsync();
                if (!document.IsError)
                {
                    var tokenEndpoint = document.TokenEndpoint;

                    //Task<TokenResponse> RequestAuthorizationCodeTokenAsync(AuthorizationCodeTokenRequest)
                    //Task<TokenResponse> RequestClientCredentialsTokenAsync(ClientCredentialsTokenRequest)
                    //Task<TokenResponse> RequestDeviceTokenAsync(DeviceTokenRequest)
                    //Task<TokenResponse> RequestPasswordTokenAsync(PasswordTokenRequest)
                    //Task<TokenResponse> RequestRefreshTokenAsync(RefreshTokenRequest)
                    //Task<TokenResponse> RequestTokenAsync(TokenRequest)

                    var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                    {
                        RequestUri = new Uri(tokenEndpoint),
                        ClientId = "EmergencyResponseService_App",
                        UserName = "admin",
                        Password = ""
                    });

                    if (!tokenResponse.IsError)
                    {
                        Console.WriteLine(tokenResponse);
                        return $"{tokenResponse.TokenType} {tokenResponse.AccessToken}";
                    }
                }
            }

            return "";
        }

        private static async Task<bool> TestToken(string token)
        {
            using (var httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:44362"), Timeout = TimeSpan.FromSeconds(5)})
            {
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    new[] {string.IsNullOrEmpty(token) ? "12" : token});

                var response = await httpClient.GetAsync("/api/multi-tenancy/tenants");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return true;
                }
            }

            return false;
        }
    }
}
