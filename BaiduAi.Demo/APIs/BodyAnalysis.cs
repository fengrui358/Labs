using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class BodyAnalysis
    {
        private readonly Baidu.Aip.BodyAnalysis.Body _client;
        private readonly ILogger _logger;

        public BodyAnalysis(IOptions<ApiConfig> apiConfig, ILogger<BodyAnalysis> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.BodyAnalysis.Body(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> BodyDetect(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = File.ReadAllBytes(filePath);
                    // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获

                    var result = _client.BodyAnalysis(image);
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
