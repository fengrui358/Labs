using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonResolveTest
{
    class Program
    {
        static void Main()
        {
            var stream = typeof(Program).Assembly.GetManifestResourceStream("JsonResolveTest.天气的Json格式.json");

            if (stream != null)
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);

                var jsonObj = JObject.Parse(Encoding.UTF8.GetString(bytes));
                var todayForecast = jsonObj.SelectToken("query.results.channel.item.forecast[0]");

                //湿度
                var humidity = (string)jsonObj.SelectToken("query.results.channel.atmosphere.humidity");

                //最高温度
                var highTemperature = (string)todayForecast.SelectToken("high");

                //最低温度
                var lowTemperature = (string)todayForecast.SelectToken("low");

                Console.WriteLine($"最高温：{highTemperature}");
                Console.WriteLine($"最低温：{lowTemperature}");
                Console.WriteLine($"湿度：{humidity}");
            }

            Console.ReadLine();
        }
    }
}