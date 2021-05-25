using Microsoft.Extensions.Configuration;

namespace BaiduAi.Demo.APIs
{
    public class Orc
    {
        public Orc(IConfiguration configuration)
        {
            var config = configuration.Get<ApiConfig>();
        }
    }
}
