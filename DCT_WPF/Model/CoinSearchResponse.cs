using System.Text.Json.Serialization;

namespace DCT_WPF.Model
{
    public class CoinSearchResponse
    {
        [JsonPropertyName("coins")]
        public List<CoinSearchResult> Coins { get; set; }
    }
}
