using DCT_WPF.Model;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace DCT_WPF.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Coin>> GetNCoins(int? N = null)
        {
            string url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd";

            _httpClient.DefaultRequestHeaders.Clear();
            string apiKey = ConfigurationManager.AppSettings["CoinGeckoApiKey"];
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);


            var response = await _httpClient.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var coins = JsonSerializer.Deserialize<List<Coin>>(response, options);

            return N.HasValue? coins.Take(N.Value).ToList() : coins;
        }

        public async Task<List<MarketInfo>> GetCoinMarketsAsync(string coinId)
        {
            string url = $"https://api.coingecko.com/api/v3/coins/{coinId}/tickers";

            _httpClient.DefaultRequestHeaders.Clear();
            string apiKey = ConfigurationManager.AppSettings["CoinGeckoApiKey"];
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);

            var response = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);
            var tickers = doc.RootElement.GetProperty("tickers");

            return tickers.EnumerateArray()
                .Take(5)
                .Select(t => new MarketInfo
                {
                    MarketName = t.GetProperty("market").GetProperty("name").GetString(),
                    Pair = $"{t.GetProperty("base").GetString()}/{t.GetProperty("target").GetString()}",
                    Price = t.GetProperty("last").GetDecimal(),
                    TradeUrl = t.GetProperty("trade_url").GetString()
                })
                .ToList();
        }

        public async Task<Coin?> GetCoinByNameOrId(string input)
        {
            string searchUrl = $"https://api.coingecko.com/api/v3/search?query={input}";
            _httpClient.DefaultRequestHeaders.Clear();
            string apiKey = ConfigurationManager.AppSettings["CoinGeckoApiKey"];
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);

            var searchResponse = await _httpClient.GetStringAsync(searchUrl);
            var searchJson = JsonSerializer.Deserialize<CoinSearchResponse>(searchResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (searchJson?.Coins == null || searchJson.Coins.Count == 0)
                return null;

            var bestMatch = searchJson.Coins
                .FirstOrDefault(c => string.Equals(c.Symbol, input, StringComparison.OrdinalIgnoreCase)
                                  || string.Equals(c.Id, input, StringComparison.OrdinalIgnoreCase))
                ?? searchJson.Coins.First();

            string marketsUrl = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={bestMatch.Id}";
            var marketsResponse = await _httpClient.GetStringAsync(marketsUrl);

            var coins = JsonSerializer.Deserialize<List<Coin>>(marketsResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return coins?.FirstOrDefault();
        }

    }
}
