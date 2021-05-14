using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.Interfaces.HttpClients;
using AspnetCoreWebApiLab.Services.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// HttpClient Test
    /// </summary>
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GitHubService _gitHubService;
        private readonly IGithubClient _githubClient;

        public HttpClientController(IHttpClientFactory httpClientFactory, GitHubService gitHubService, IGithubClient githubClient)
        {
            _httpClientFactory = httpClientFactory;
            _gitHubService = gitHubService;
            _githubClient = githubClient;
        }

        [HttpGet("api/GetAspNetCoreDocs")]
        public async Task<ActionResult<string>> GetAspNetCoreDocs()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "dotnet/AspNetCore.Docs");

            var client = _httpClientFactory.CreateClient("github");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Content(result);
            }
            else
            {
                throw new Exception("获取失败");
            }
        }

        [HttpGet("api/GetAspNetCoreDocs2")]
        public async Task<ActionResult<string>> GetAspNetCoreDocs2()
        {
            return await _gitHubService.GetAspNetDocsIssues();
        }

        [HttpGet("api/GetGithubMainPage")]
        public async Task<ActionResult<string>> GetGithubMainPage()
        {
            return await _githubClient.GetMainPage();
        }
    }
}
