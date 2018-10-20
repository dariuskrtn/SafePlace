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
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _IPAdress;
        public string IPAdress
        {
            get
            {
                return _IPAdress;
            }
            set
            {
                _IPAdress = value;
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
