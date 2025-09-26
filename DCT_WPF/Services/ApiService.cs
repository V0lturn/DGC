using DCT_WPF.Model;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace DCT_WPF.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Coin>> GetTop10Coins()
        {
            string url = "https://api.coingecko.com/api/v3/coins/markets" +
             "?vs_currency=usd&order=market_cap_desc&per_page=10&page=1";

            _httpClient.DefaultRequestHeaders.Clear();

            string apiKey = ConfigurationManager.AppSettings["CoinGeckoApiKey"];
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);


            var response = await _httpClient.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var coins = JsonSerializer.Deserialize<List<Coin>>(response, options);

            return coins;
        }
    }
}
