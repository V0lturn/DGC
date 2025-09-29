using System.Text.Json.Serialization;

namespace DCT_WPF.Model
{
    public class Coin
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("total_volume")]
        public decimal TotalVolume { get; set; }

        [JsonPropertyName("price_change_24h")]
        public decimal? PriceChange { get; set; }
    }
}
