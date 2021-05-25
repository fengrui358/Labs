using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class Orc
    {
        private readonly Baidu.Aip.Ocr.Ocr _client;
        private readonly ILogger _logger;

        public Orc(IOptions<ApiConfig> apiConfig, ILogger<Orc> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.Ocr.Ocr(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY);
        }

        public async Task<string> OrcBasic(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = File.ReadAllBytes(filePath);
                    // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获

                    var result = _client.GeneralBasic(image);
                    Console.WriteLine(result);
                    // 如果有可选参数
                    var options = new Dictionary<string, object>{
                        {"language_type", "CHN_ENG"},
                        {"detect_direction", "true"},
                        {"detect_language", "true"},
                        {"probability", "true"}
                    };
                    // 带参数调用通用文字识别, 图片参数为本地图片
                    result = _client.GeneralBasic(image, options);

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
