using System.Text.Json;

namespace Core.Framework.Serializers
{
    public static class JsonSerializer
    {
        private static JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string Serialize(object value)
        {
            return System.Text.Json.JsonSerializer.Serialize(value, serializeOptions);
        }

        public static T Deserialize<T>(string jsonString)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, serializeOptions);
        }
    }
}
