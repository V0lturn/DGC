using DCT_WPF.Commands;
using DCT_WPF.Model;
using DCT_WPF.Services;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Input;

namespace DCT_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private readonly ApiService _coinService;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private Coin _searchResult;
        public Coin SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
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
            SearchResult = null;
            IsPopupOpen = false;

            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            var coin = await _coinService.GetCoinByNameOrId(SearchText);
            if (coin != null)
            {
                SearchResult = coin;
                IsPopupOpen = true;
            }
            else
            {
                MessageBox.Show("Error has occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
