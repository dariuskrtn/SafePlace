using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SafePlace.Views.MainWindowView
{
    class MainWindowViewModel : BaseViewModel
    {

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

        public ObservableCollection<string> FloorList { get; } = new ObservableCollection<string>();

    }
}
