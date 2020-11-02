using Newtonsoft.Json;

namespace JsonLabs.JsonNetLab
{
    public class MockClass
    {
        public string Str1 { get; set; }

        [JsonConverter(typeof(BoolJsonConverter))]
        public string BoolStr { get; set; }
    }
}
