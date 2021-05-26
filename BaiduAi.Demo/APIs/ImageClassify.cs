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
    public class ImageClassify
    {
        private readonly Baidu.Aip.ImageClassify.ImageClassify _client;
        private readonly ILogger _logger;

        public ImageClassify(IOptions<ApiConfig> apiConfig, ILogger<ImageClassify> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.ImageClassify.ImageClassify(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> ImageClassifyDetect(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = File.ReadAllBytes(filePath);

                    var result = _client.AdvancedGeneral(image);
                    Console.WriteLine(result);

                    // 如果有可选参数
                    var options = new Dictionary<string, object>{
                        {"baike_num", 5}
                    };
                    // 带参数调用通用物体识别
                    result = _client.AdvancedGeneral(image, options);
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
