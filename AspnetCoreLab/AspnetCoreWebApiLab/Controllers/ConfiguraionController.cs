using System;
using AspnetCoreWebApiLab.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 配置服务
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguraionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<ConfigurationTestModel> _configurationTestModelOptions;

        public ConfiguraionController(IConfiguration configuration, IOptions<ConfigurationTestModel> configurationTestModelOptions)
        {
            _configuration = configuration;

            // 注入配置，注入之后的配置不会变化
            _configurationTestModelOptions = configurationTestModelOptions;
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

        /// <summary>
        /// 获取配置的 ConfigurationTest 节点配置的 Json 对象
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetConfigurationTestModel))]
        public ConfigurationTestModel GetConfigurationTestModel()
        {
            //var configurationTestModel = new ConfigurationTestModel();
            //_configuration.GetSection("ConfigurationTest").Bind(configurationTestModel);

            //return configurationTestModel;

            var x = _configuration.GetSection("ConfigurationTest").Get<ConfigurationTestModel>();

            return x;
        }

        /// <summary>
        /// 获取指定配置节点的子项
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetConfigurationChildren))]
        public ActionResult<string> GetConfigurationChildren(string sectionKey)
        {
            var selection = _configuration.GetSection(sectionKey);
            if (!selection.Exists())
            {
                return new ActionResult<string>($"指定节点 {sectionKey} 不存在");
            }

            var children = selection.GetChildren();

            var s = "";
            int i = 0;

            foreach (var subSection in children)
            {
                s += $"key{++i}, key:{subSection.Key}，value:{subSection.Value}{Environment.NewLine}";
            }
            return Content(s);
        }
    }
}
