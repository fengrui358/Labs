using System;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            return Json(new { route = Request.RouteValues, query = Request.Query });
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
        public IActionResult GetFile(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "夏令时技术预研.txt";
            }

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                //return File(System.IO.File.ReadAllBytes(path), "text/xml");
                return File(System.IO.File.OpenRead(path), "text/xml", true);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetPhysicalFile))]
        public IActionResult GetPhysicalFile(string fileName = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "夏令时技术预研.txt";
            }

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName);
            if (System.IO.File.Exists(path))
            {
                return PhysicalFile(Path.Combine(_webHostEnvironment.ContentRootPath, "MyStaticFiles", fileName), "text/xml");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
