using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 路由信息
    /// </summary>
    [Route("api/[controller]")]
    public class RouteController : Controller
    {
        /// <summary>
        /// 测试获取路由信息
        /// </summary>
        /// <param name="test"></param>
        /// <param name="testNum"></param>
        /// <returns></returns>
        [HttpGet("/GetRoute/GetRouteValuesAndQuery")]

        public IActionResult GetRouteValues(string test, int testNum)
        {
            var values = JsonConvert.SerializeObject(new {route = Request.RouteValues, query = Request.Query});

            return Content(values);
        }
    }
}
