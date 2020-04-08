using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new[] {"http://localhost:5000/"};
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            while (true)
            {
                var listenerContext = await _httpListener.GetContextAsync();
                // 打印状态行: 请求方法, URL, 协议版本
                Console.WriteLine("{0} {1} HTTP/{2}",
                    listenerContext.Request.HttpMethod,
                    listenerContext.Request.RawUrl,
                    listenerContext.Request.ProtocolVersion);

                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);
                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }
}