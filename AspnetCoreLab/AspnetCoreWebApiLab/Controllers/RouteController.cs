using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreWebApiLab.Controllers
{
    /// <summary>
    /// 路由信息
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RouteController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 测试获取路由信息
        /// </summary>
        /// <param name="test"></param>
        /// <param name="testNum"></param>
        /// <returns></returns>
        [HttpGet("/GetRoute/GetRouteValuesAndQuery")]

        public IActionResult GetRouteValues(string test, int testNum)
        {
            return Content(System.Text.Json.JsonSerializer.Serialize(new { route = Request.RouteValues, query = Request.Query }));
        }

        /// <summary>
        /// 获取终结点
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetEndPoint))]
        public ActionResult<string> GetEndPoint()
        {
            var endpoint = HttpContext.GetEndpoint();
            return endpoint?.DisplayName;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetFile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFile(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "夏令时技术预研.txt";
            }

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                //return File(System.IO.File.ReadAllBytes(path), MediaTypeNames.Text.Plain);
                return File(System.IO.File.OpenRead(path), MediaTypeNames.Text.Plain, true);
            }
            else
            {
                return NotFound(new ProblemDetails { Title = "找不到-Title", Detail = "找不到-Detail", Instance = "找不到-Instance", Type = "找不到-Type" });
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetPhysicalFile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPhysicalFile(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "夏令时技术预研.txt";
            }

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                return PhysicalFile(Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName), MediaTypeNames.Text.Plain);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
