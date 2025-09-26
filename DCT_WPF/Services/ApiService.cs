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
    }
}
