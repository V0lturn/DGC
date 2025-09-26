using DCT_WPF.Model;
using DCT_WPF.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DCT_WPF.View
{
    public partial class DetailsView : UserControl
    {
        private readonly ApiService _apiService = new ApiService();
        public DetailsView()
        {
            InitializeComponent();
        }

        private async void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander && expander.DataContext is Coin coin)
            {
                if (expander.Content is StackPanel sp && sp.Children.OfType<ItemsControl>().FirstOrDefault() is ItemsControl ic)
                {
                    if (ic.Items.Count == 0)
                    {
                        var markets = await _apiService.GetCoinMarketsAsync(coin.Id);
                        foreach (var market in markets.Take(5))
                        {
                            ic.Items.Add(market);
                        }
                    }
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

    }
}
    