using DCT_WPF.Model;
using DCT_WPF.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DCT_WPF.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly ApiService _apiService = new ApiService();
        public ObservableCollection<Coin> Coins { get; set; } = new ObservableCollection<Coin>();

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        public HomeViewModel()
        {
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var coins = await _apiService.GetTop10Coins();

                foreach (var coin in coins)
                {
                    Coins.Add(coin);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
