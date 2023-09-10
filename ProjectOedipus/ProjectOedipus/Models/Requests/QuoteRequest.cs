using System.Text.Json.Serialization;

namespace Models.Requests
{
    public class QuoteRequest
    {
        [JsonPropertyName("q")]
        public string Quote { get; set; }

        [JsonPropertyName("a")]
        public string Author { get; set; }
    }
}