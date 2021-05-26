using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class ImageEffects
    {
        private readonly Baidu.Aip.ImageProcess.ImageProcess _client;
        private readonly ILogger _logger;

        public ImageEffects(IOptions<ApiConfig> apiConfig, ILogger<ImageEffects> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.ImageProcess.ImageProcess(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> ImageQualityEnhance(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = File.ReadAllBytes(filePath);

                    var result = _client.ImageQualityEnhance(image);
                    Console.WriteLine(result);

                    var imageBase64 = result.Value<string>("image");

                    File.WriteAllBytes("无损放大图片.jpg", Convert.FromBase64String(imageBase64));

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
