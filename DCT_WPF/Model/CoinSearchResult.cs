using System.Text.Json.Serialization;

namespace DCT_WPF.Model
{
    public class CoinSearchResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
