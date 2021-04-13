using System.Text.Json.Serialization;

namespace Nasa.Business.Exceptions
{
    public class ExceptionResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
