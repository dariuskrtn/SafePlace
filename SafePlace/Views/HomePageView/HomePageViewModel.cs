using SafePlace.Models;
using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SafePlace.Views.HomePageView
{
    class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<Camera> Cameras { set; get; }

        private ICommand _cameraClickCommand;
        public ICommand CameraClickCommand {
            get
            {
                return _cameraClickCommand;
            }
            set
            {
                _cameraClickCommand = value;
                NotifyPropertyChanged();
            }

        }

        private BitmapImage _cameraImage;
        public BitmapImage CameraImage
        {
            get
            {
                return _cameraImage;
            }
            set
            {
                _cameraImage = value;
                NotifyPropertyChanged();
            }
        }

        private BitmapImage _currentFloorImage;
        public BitmapImage CurrentFloorImage
        {
            get
            {
                return _currentFloorImage;
            }
            set
            {
                _currentFloorImage = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> FloorList { get; } = new ObservableCollection<string>();

    }
}
