using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPageViewModel : BaseViewModel
    {

        private ICommand _floorButtonClickCommand;
        public ICommand FloorButtonClickCommand
        {
            get
            {
                return _floorButtonClickCommand;
            }
            set
            { 
                _floorButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }

        private BitmapImage _floorImage;
        public BitmapImage FloorImage
        {
            get
            {
                return _floorImage;
            }
            set
            {
                _floorImage = value;
                NotifyPropertyChanged();
            }
        }
    }

}
