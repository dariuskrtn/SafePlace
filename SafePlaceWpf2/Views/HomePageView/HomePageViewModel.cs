using SafePlace.Models;
using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SafePlaceWpf.Views.HomePageView
{
    class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<CameraViewModel> Cameras { set; get; } = new ObservableCollection<CameraViewModel>();
        public ObservableCollection<Floor> Floors { set; get; } = new ObservableCollection<Floor>();
        //Is displayed above the floor plan.
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
        private CameraViewModel _selectedCamera;
        public CameraViewModel SelectedCamera
        {
            get
            {
                return _selectedCamera;
            }
            set
            {
                _selectedCamera = value;
                NotifyPropertyChanged();
            }
        }
        //Required for tracking which floor is currently shown.
        private int _currentFloor;
        public int CurrentFloor
        {
            get
            {
                return _currentFloor;
            }
            set
            {
                _currentFloor = value;
                NotifyPropertyChanged();
            }
        }
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
        //a command for changing the current floor based on which floor is clicked.
        private ICommand _floorListClickCommand;
        public ICommand FloorListClickCommand
        {
            get
            {
                return _floorListClickCommand;
            }
            set
            {
                _floorListClickCommand = value;
                NotifyPropertyChanged();
            }

        }
        //If a behavior to the floor changing buttons is applied, one command for changing floors should be enough.
        private ICommand _floorUpCommand;
        public ICommand FloorUpCommand
        {
            get
            {
                return _floorUpCommand;
            }
            set
            {
                _floorUpCommand = value;
                NotifyPropertyChanged();
            }

        }

        private ICommand _floorDownCommand;
        public ICommand FloorDownCommand
        {
            get
            {
                return _floorDownCommand;
            }
            set
            {
                _floorDownCommand = value;
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
        //Intended to be linked to the visibility of the listView, containing identified people.
        //The list should appear only when a camera is clicked and ShowList becomes true.
        public Boolean ShowList { set; get; }

    }
}
