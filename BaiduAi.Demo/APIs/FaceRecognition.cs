using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class FaceRecognition
    {
        private readonly Baidu.Aip.Face.Face _client;
        private readonly ILogger _logger;

        public FaceRecognition(IOptions<ApiConfig> apiConfig, ILogger<Orc> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.Face.Face(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) {Timeout = 60000};
        }

        public async Task<string> FaceDetect(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var image = Convert.ToBase64String(File.ReadAllBytes(filePath));

                    var imageType = "BASE64";

                    // 调用人脸检测，可能会抛出网络等异常，请使用try/catch捕获
                    var result = _client.Detect(image, imageType);
                    Console.WriteLine(result);
                    // 如果有可选参数
                    //var options = new Dictionary<string, object>
                    //{
                    //    {"face_field", "age"},
                    //    {"max_face_num", 2},
                    //    {"face_type", "LIVE"},
                    //    {"liveness_control", "LOW"}
                    //};
                    //// 带参数调用人脸检测
                    //result = _client.Detect(image, imageType, options);
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
