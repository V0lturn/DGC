using System.Collections.ObjectModel;

namespace DCT_WPF.Model
{
    public class CoinsWithMarket : Coin
    {
        public ObservableCollection<MarketInfo> Markets { get; set; } = new ObservableCollection<MarketInfo>();
    }
}
