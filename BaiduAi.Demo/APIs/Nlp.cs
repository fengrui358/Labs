using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BaiduAi.Demo.APIs
{
    public class Nlp
    {
        private readonly Baidu.Aip.Nlp.Nlp _client;
        private readonly ILogger _logger;

        public Nlp(IOptions<ApiConfig> apiConfig, ILogger<Nlp> logger)
        {
            _logger = logger;
            _client = new Baidu.Aip.Nlp.Nlp(apiConfig.Value.API_KEY, apiConfig.Value.SECRET_KEY) { Timeout = 60000 };
        }

        public async Task<string> Lexer()
        {
            return await Task.Run(() =>
            {
                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    var result = _client.Lexer("我正在测试百度云只能AI平台的语言处理功能。");
                    Console.WriteLine(result);

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
