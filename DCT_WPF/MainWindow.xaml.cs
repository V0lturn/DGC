using DCT_WPF.ViewModel;
using System.Windows;

namespace DCT_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}