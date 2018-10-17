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



    }

}
