using DCT_WPF.Model;
using DCT_WPF.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace DCT_WPF.ViewModel
{
    public class DetailsViewModel : BaseViewModel
    {
        private readonly ApiService _apiService = new ApiService();
        public ObservableCollection<Coin> Coins { get; set; } = new ObservableCollection<Coin>();

        public DetailsViewModel()
        {
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var coins = await _apiService.GetNCoins();

                foreach (var coin in coins)
                {
                    Coins.Add(coin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error has been occured: {ex.Message}", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
