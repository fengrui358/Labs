using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class Speech
    {
        private readonly Baidu.Aip.Speech.Tts _client;
        private readonly ILogger _logger;

        public Speech(IOptions<ApiConfig> apiConfig, ILogger<Speech> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.Speech.Tts(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> Tts()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // 可选参数
                    var options = new Dictionary<string, object>()
                    {
                        {"spd", 5}, // 语速
                        {"vol", 7}, // 音量
                        {"per", 4}  // 发音人，4：情感度丫丫童声
                    };
                    var result = _client.Synthesis("蓝色IV级告警：龙泉中心局出局往天悦国际局方向，LQYZXJ/GJ028号光缆东经104.280640北纬30.578756中继光缆发生中断，已派龙泉01组处置", options);
                    if (result.ErrorCode == 0)  // 或 result.Success
                    {
                        File.WriteAllBytes("合成的语音文件本地存储地址.mp3", result.Data);
                    }

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
