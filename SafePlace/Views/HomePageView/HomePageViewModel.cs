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

namespace SafePlace.Views.HomePageView
{
    class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<Camera> Cameras { set; get; } = new ObservableCollection<Camera>();
        public ObservableCollection<Person> SpottedPeople { set; get; } = new ObservableCollection<Person>();
        //Required for tracking which floor is currently shown.
        public int CurrentFloor { set; get; }
        public ObservableCollection<Floor> Floors { set; get; } = new ObservableCollection<Floor>();
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

        public ObservableCollection<string> FloorList { get; } = new ObservableCollection<string>();

    }
}
