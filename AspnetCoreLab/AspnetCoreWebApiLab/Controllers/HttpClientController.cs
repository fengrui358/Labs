using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.Services.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// HttpClient Test
    /// </summary>
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class HttpClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GitHubService _gitHubService;

        public HttpClientController(IHttpClientFactory httpClientFactory, GitHubService gitHubService)
        {
            _httpClientFactory = httpClientFactory;
            _gitHubService = gitHubService;
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
    }
}
