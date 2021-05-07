using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 配置服务
    /// </summary>
    [Route("api/[controller]")]
    public class ConfiguraionController
    {
        private readonly IConfiguration _configuration;

        public ConfiguraionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProviders")]
        public string GetProviders()
        {
            var result = "";

            foreach (var configurationProvider in ((IConfigurationRoot)_configuration).Providers)
            {
                result += configurationProvider.ToString();
            }

            return result;
        }

        /// <summary>
        /// 获取单独配置
        /// </summary>
        /// <param name="key">配置 key</param>
        /// <returns></returns>
        [HttpGet(nameof(GetConfiguration))]
        public string GetConfiguration(string key)
        {
            return _configuration[key];
        }
    }
}
