using System;
using Newtonsoft.Json;

namespace JsonLabs.JsonNetLab
{
    public class BoolJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is string str)
            {
                if (str.Equals("YES", StringComparison.CurrentCultureIgnoreCase))
                {
                    writer.WriteValue(true);
                }
                else if (str.Equals("NO", StringComparison.CurrentCultureIgnoreCase))
                {
                    writer.WriteValue(false);
                }
                else
                {
                    writer.WriteNull();
                }
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Boolean)
            {
                if (bool.Parse(reader.Value?.ToString()))
                {
                    return "YES";
                }
                else
                {
                    return "NO";
                }
            }

            return "undefined";
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }
    }
}
