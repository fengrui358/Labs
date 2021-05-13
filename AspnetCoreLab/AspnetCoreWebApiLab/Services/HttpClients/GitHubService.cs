using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetCoreWebApiLab.Services.HttpClients
{
    public class GitHubService
    {
        private readonly HttpClient _client;

        public GitHubService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");

            _client = client;
        }

        public async Task<string> GetAspNetDocsIssues()
        {
            var response = await _client.GetAsync(
                "/repos/dotnet/AspNetCore.Docs/issues?state=open&sort=created&direction=desc");

            response.EnsureSuccessStatusCode();

            var responseStr = await response.Content.ReadAsStringAsync();
            return responseStr;
        }
    }
}
