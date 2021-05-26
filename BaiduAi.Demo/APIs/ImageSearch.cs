using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class ImageSearch
    {
        private readonly Baidu.Aip.ImageSearch.ImageSearch _client;
        private readonly ILogger _logger;

        public ImageSearch(IOptions<ApiConfig> apiConfig, ILogger<ImageSearch> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.ImageSearch.ImageSearch(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> ImageSearchDetect(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = File.ReadAllBytes(filePath);

                    var result = _client.SimilarSearch(image);
                    Console.WriteLine(result);

                    //// 如果有可选参数
                    //var options = new Dictionary<string, object>{
                    //    {"tags", "100,11"},
                    //    {"tag_logic", "0"},
                    //    {"pn", "100"},
                    //    {"rn", "250"}
                    //};
                    //// 带参数调用通用物体识别
                    //result = _client.SimilarSearch(image, options);
                    return result.ToString();
                }
                catch (Exception e)
                {
                    _logger.LogError(null, e);
                    throw;
                }
            });
        }
    }
}
