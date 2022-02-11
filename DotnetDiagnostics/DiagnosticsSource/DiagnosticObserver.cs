using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.AspNetCore.Http.Extensions;

namespace DiagnosticsSource
{
    public class DiagnosticObserver : IObserver<KeyValuePair<string, object?>>
    {
        private ConcurrentDictionary<string, long> _startTimes = new ConcurrentDictionary<string, long>();

        public void OnCompleted()
        {
            Console.WriteLine("OnCompleted");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"OnError：{error}");
        }

        public void OnNext(KeyValuePair<string, object?> pair)
        {
            // 这里消费诊断数据
            Console.WriteLine($"DiagnosticObserver {pair.Key}-{pair.Value}");

            //获取httpContext
            var context = pair.Value.GetType().GetTypeInfo().GetDeclaredProperty("httpContext")
                ?.GetValue(pair.Value) as DefaultHttpContext;
            //获取timestamp
            var timestamp = pair.Value.GetType().GetTypeInfo().GetDeclaredProperty("timestamp")
                ?.GetValue(pair.Value) as long?;

            switch (pair.Key)
            {
                case "Microsoft.AspNetCore.Hosting.BeginRequest":
                    Console.WriteLine($"Request {context.TraceIdentifier} Begin:{context.Request.GetDisplayUrl()}");
                    _startTimes.TryAdd(context.TraceIdentifier, timestamp.Value);//记录请求开始时间
                    break;
                case "Microsoft.AspNetCore.Hosting.EndRequest":
                    _startTimes.TryGetValue(context.TraceIdentifier, out long startTime);
                    var elapsedMs = (timestamp - startTime) / TimeSpan.TicksPerMillisecond;//计算耗时
                    Console.WriteLine(
                        $"Request {context.TraceIdentifier} End: Status Code is {context.Response.StatusCode},Elapsed {elapsedMs}ms");
                    _startTimes.TryRemove(context.TraceIdentifier, out _);
                    break;
            }
        }
    }
}