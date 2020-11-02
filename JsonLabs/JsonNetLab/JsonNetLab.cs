using System;
using Newtonsoft.Json;

namespace JsonLabs.JsonNetLab
{
    public class JsonNetLab
    {
        public void Run()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var c = new MockClass
            {
                Str1 = "Str1",
                BoolStr = "YES"
            };

            var s = JsonConvert.SerializeObject(c, jsonSerializerSettings);
            Console.WriteLine($"原始json {s}");
            var d = JsonConvert.DeserializeObject<MockClass>(s, jsonSerializerSettings);
            Console.WriteLine($"目标属性值 {d.BoolStr}");

            c.BoolStr = "NO";
            s = JsonConvert.SerializeObject(c, jsonSerializerSettings);
            Console.WriteLine($"原始json {s}");
            d = JsonConvert.DeserializeObject<MockClass>(s, jsonSerializerSettings);
            Console.WriteLine($"目标属性值 {d.BoolStr}");

            //设置 NullValueHandling = NullValueHandling.Ignore 后无法重新触发 JsonConvert 的属性接口
            c.BoolStr = null;
            s = JsonConvert.SerializeObject(c);
            Console.WriteLine($"原始json {s}");
            d = JsonConvert.DeserializeObject<MockClass>(s);
            Console.WriteLine($"目标属性值 {d.BoolStr}");
        }
    }
}
