using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace IdentityServerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TestToken("");

            var token = await GetTokenAsync();
            await TestToken(token);

            await GetUserInfo(token);

            var code = await GetCodeAsync();
            await TestToken(code);

            await GetUserInfo(code);

            Console.ReadLine();
        }

        private static async Task<string> GetTokenAsync()
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

                    var request = new PasswordTokenRequest
                    {
                        RequestUri = new Uri(tokenEndpoint),
                        ClientId = "EmergencyResponseService_App",
                        UserName = "18888888887",
                        Password = "123456"
                    };

                    request.Headers.Add("__tenant", "39fe0e34-6c58-c5d7-ed25-0b0c9275a1f0");

                    var tokenResponse = await httpClient.RequestPasswordTokenAsync(request);

                    if (!tokenResponse.IsError)
                    {
                        ValidToken(tokenResponse.AccessToken);

                        Console.WriteLine(tokenResponse);
                        return $"{tokenResponse.TokenType} {tokenResponse.AccessToken}";
                    }
                }
            }

            return "";
        }

        private static async Task<string> GetCodeAsync()
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:44362") })
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

                    var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                    {
                        RequestUri = new Uri(tokenEndpoint),
                        ClientId = "EmergencyResponseService_App",
                        ClientSecret= "",
                    });

                    if (!tokenResponse.IsError)
                    {
                        ValidToken(tokenResponse.AccessToken);

                        Console.WriteLine(tokenResponse);
                        return $"{tokenResponse.TokenType} {tokenResponse.AccessToken}";
                    }
                }
            }

            return "";
        }

        private static async Task GetUserInfo(string token)
        {
            using (var httpClient = new HttpClient {BaseAddress = new Uri("http://localhost:44362")})
            {
                var document = await httpClient.GetDiscoveryDocumentAsync();
                if (!document.IsError)
                {
                    token = token.Remove(0, 7);
                    var request = new UserInfoRequest
                    {
                        RequestUri = new Uri(document.UserInfoEndpoint),
                        Token = token
                    };

                    var userResponse = await httpClient.GetUserInfoAsync(request);

                    if (!userResponse.IsError)
                    {
                        Console.WriteLine(userResponse.Raw);
                    }
                }
            }
        }

        private static void ValidToken(string token)
        {
            var c = new JwtSecurityToken(token);
            Console.WriteLine(JsonConvert.SerializeObject(c));
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
