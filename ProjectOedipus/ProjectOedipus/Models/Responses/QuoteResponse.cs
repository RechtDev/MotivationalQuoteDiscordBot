using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ProjectOedipus.Models.Responses
{
    public class QuoteResponse
    {
        [JsonProperty("q")]
        [JsonPropertyName("q")]
        public string Quote { get; set; }

        [JsonProperty("a")]
        [JsonPropertyName("a")]
        public string Author { get; set; }
    }
}