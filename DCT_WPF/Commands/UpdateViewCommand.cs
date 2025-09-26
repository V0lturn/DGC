using DCT_WPF.ViewModel;
using System.Windows.Input;

namespace DCT_WPF.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            string? paramValue = parameter?.ToString();

            switch (paramValue)
            {
                case "Home":
                    viewModel.SelectedViewModel = new HomeViewModel();
                    break;
                case "Details":
                    viewModel.SelectedViewModel = new DetailsViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
