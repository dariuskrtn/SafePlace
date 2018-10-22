using SafePlace.Models;
using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPageViewModel : BaseViewModel
    {

        #region Settings page properties

        public ObservableCollection<Camera> CameraCollection { set; get; } = new ObservableCollection<Camera>();
        public ObservableCollection<Floor> FloorCollection { set; get; } = new ObservableCollection<Floor>();

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

        private string _floorName;
        public string FloorName
        {
            get
            {
                return _floorName;
            }
            set
            {
                _floorName = value;
                NotifyPropertyChanged();
            }
        }

        // Not sure if name is good, sounds like a method
        private bool _showPopUp;
        public bool ShowPopUp
        {
            get
            {
                return _showPopUp;
            }
            set
            {
                _showPopUp = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        private ICommand _floorImageClickCommand;
        public ICommand FloorImageClickCommand
        {
            get
            {
                return _floorImageClickCommand;
            }
            set
            {
                _floorImageClickCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _cameraClickCommand;
        public ICommand CameraClickCommand
        {
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

        #region Buttons Commands
        private ICommand _editButtonClickCommand;
        public ICommand EditButtonClickCommand
        {
            get
            {
                return _editButtonClickCommand;
            }
            set
            {
                _editButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _addCameraButtonClickCommand;
        public ICommand AddCameraButtonClickCommand
        {
            get
            {
                return _addCameraButtonClickCommand;
            }
            set
            {
                _addCameraButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _chooseImageButtonClickCommand;
        public ICommand ChooseImageButtonClickCommand
        {
            get
            {
                return _chooseImageButtonClickCommand;
            }
            set
            {
                _chooseImageButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }

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

        private ICommand _cancelButtonClickCommand;
        public ICommand CancelButtonClickCommand
        {
            get
            {
                return _cancelButtonClickCommand;
            }
            set
            {
                _cancelButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _deleteButtonClickCommand;
        public ICommand DeleteButtonClickCommand
        {
            get
            {
                return _deleteButtonClickCommand;
            }
            set
            {
                _deleteButtonClickCommand = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region pop up properties
        //Binding to the pop up contents.

        // Indicates if blue camera is shown
        private bool _isNewCameraShown;
        public bool IsNewCameraShown
        {
            get
            {
                return _isNewCameraShown;
            }
            set
            {
                _isNewCameraShown = value;
                NotifyPropertyChanged();
            }
        }
        private string _cameraName;
        public string CameraName
        {
            get
            {
                return _cameraName;
            }
            set
            {
                _cameraName = value;
                NotifyPropertyChanged();
            }
        }
        private string _IPAddress;
        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;
                NotifyPropertyChanged();
            }
        }
        private int _positionX;
        public int PositionX
        {
            get
            {
                return _positionX;
            }
            set
            {
                _positionX = value;
                NotifyPropertyChanged();
            }
        }
        private int _positionY;
        public int PositionY
        {
            get
            {
                return _positionY;
            }
            set
            {
                _positionY = value;
                NotifyPropertyChanged();
            }
        }
        private ICommand _cameraAddCommand;
        public ICommand CameraAddCommand
        {
            get
            {
                return _cameraAddCommand;
            }
            set
            {
                _cameraAddCommand = value;
                NotifyPropertyChanged();
            }

        }

        private ICommand _cameraCancelCommand;
        public ICommand CameraCancelCommand
        {
            get
            {
                return _cameraCancelCommand;
            }
            set
            {
                _cameraCancelCommand = value;
                NotifyPropertyChanged();
            }

        }
        #endregion
    }

}
