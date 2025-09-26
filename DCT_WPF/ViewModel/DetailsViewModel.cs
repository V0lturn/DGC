using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_WPF.ViewModel
{
    public class DetailsViewModel : BaseViewModel
    {
        private string _detailsInfo;

        public string DetailsInfo
        {
            get { return _detailsInfo; }
            set
            {
                _detailsInfo = value;
                OnPropertyChanged(nameof(DetailsInfo));
            }
        }

        public DetailsViewModel()
        {
            DetailsInfo = "Здесь отображаются детали";
        }
    }
}
