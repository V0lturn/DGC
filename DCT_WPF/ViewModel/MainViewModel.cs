
using DCT_WPF.Commands;
using DCT_WPF.Model;
using DCT_WPF.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace DCT_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            SelectedViewModel = new HomeViewModel();
        }

       
    }
}
