using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticsSource.Controllers
{
    /// <summary>
    /// 参考文章：
    /// https://mp.weixin.qq.com/s/bO7QpH0mvaW6i192fu_SCg
    /// https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
    /// https://www.cnblogs.com/sheng-jie/p/how-much-you-know-about-diagnostic-in-dotnet.html
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        // 2. 搞一个发布者
        private static readonly DiagnosticSource _diagnosticSource = new DiagnosticListener("System.Net.Http");

        [HttpGet(Name = "Write")]
        public string Write()
        {
            // 判断是否存在 RequestStart 的订阅者
            if (_diagnosticSource.IsEnabled("RequestStart"))
                _diagnosticSource.Write("RequestStart", new { Url = "http://clr", Request = "request" });
            return "write over";
        }

        [HttpGet("Subscribe")]
        public async Task<string> Subscribe()
        {
            var kettle = new Kettle();//初始化热水壶
            var subscribeRef = kettle.Subscribe(new Alter());//订阅

            var boilTask = kettle.StartBoilWaterAsync();//启动开始烧水任务
            var timoutTask = Task.Delay(TimeSpan.FromSeconds(15));//定义15s超时任务
            //等待，如果超时任务先返回则取消订阅
            var firstReturnTask = await Task.WhenAny(boilTask, timoutTask);
            if (firstReturnTask == timoutTask)
                subscribeRef.Dispose();

            return "Subscribe";
        }
    }
}