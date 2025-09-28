using DCT_WPF.Commands;
using DCT_WPF.Services;
using System.Windows;
using System.Windows.Input;

namespace DCT_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private string _searchText;
        private readonly ApiService _coinService;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand UpdateViewCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            SearchCommand = new RelayCommand(async _ => await SearchCoin());
            SelectedViewModel = new HomeViewModel();

            _coinService = new ApiService();
        }

        private async Task SearchCoin()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                MessageBox.Show("Enter name of the coin");
                return;
            }

            var coin = await _coinService.GetCoinByNameOrId(SearchText);
            if (coin == null)
            {
                MessageBox.Show("Coin was not found");
                return;
            }

            MessageBox.Show(
                $"Name: {coin.Name}\n" +
                $"Symbol: {coin.Symbol}\n" +
                $"Price: {coin.CurrentPrice}$\n" +
                $"Volume: {coin.TotalVolume}\n" +
                $"24h Change: {coin.PriceChange}"
            );

            SearchText = string.Empty;
        }

    }
}
