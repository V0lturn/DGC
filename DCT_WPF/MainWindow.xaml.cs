using DCT_WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace DCT_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void ChangeLanguage_BtnClick(object sender, RoutedEventArgs e)
        {
            SetLanguage(((Button)sender).Tag.ToString());
        }

        private void SetLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);

            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resdict = new ResourceDictionary()
            {
                Source = new Uri($"/Localization/Dicitionary-{language}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(resdict);

            EngButton.IsEnabled = true;
            UaButton.IsEnabled = true;

            switch (language)
            {
                case "en":
                    EngButton.IsEnabled = false;
                    break;
                case "ua":
                    UaButton.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}