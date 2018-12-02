using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using SafePlace.ViewModels;

namespace SafePlaceWpf.Views.HomePageView
{
    /*
     * Moved Home Page from MainWindow.
     * Please check if all dependencies are required and remove/add the ones which are needed.
     */
    class HomePagePresenter
    {
        private HomePageViewModel _viewModel;
        private readonly IMainService _mainService;
        private SynchronizationContext _synchronizationContext;
        private ILogger _logger;
        private IFloorService _floorService;



        private string[] Names = { "Joseph", "Johan", "John", "Jack", "Joe", "Johnattan", "Jacob", "Jason", "Jennifer", "Jay" };
        private string[] LastNames = { "Peugeot", "Ferrari", "Harrari", "Smith", "Sans", "Rutherford", "Boore", "Huxley", "Jacksondaughter", "Joestar"};
        
        public HomePagePresenter(HomePageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _mainService = mainService;
            GetServices(_mainService);
            LoadFloorList();
            BuildViewModel();
            BuildSubscriptions();
        }

        private void GetServices(IMainService mainService)
        {
            _logger = mainService.GetLoggerInstance();
            _synchronizationContext = mainService.GetSynchronizationContext();
            _floorService = mainService.GetFloorServiceInstance();
        }

        private void BuildViewModel()
        {
            BuildCommands();
            _viewModel.CameraImage = new BitmapImage(new Uri("/Images/camera.png", UriKind.Relative));
            _viewModel.CurrentFloor = 0;
            if (_viewModel.Floors.Count > 0)
                LoadFloor(_viewModel.Floors[0]);
        }

        private void BuildSubscriptions()
        {
            /*
            Observable.Merge(_mainService.GetAnalyzeServices().Select(analyzer => analyzer.GetCameraUpdateObservable())).Subscribe(cam =>
            {
                CameraViewModel viewModelToChange = null;
                foreach (var cameraViewModel in _viewModel.Cameras)
                {
                    if (cam.Guid == cameraViewModel.Guid) viewModelToChange = cameraViewModel;
                }
                if (viewModelToChange == null) return;
                UpdateCameraViewModel(cam, viewModelToChange);
            });
            */
        }
        
        public void UpdateCameraViewModel(Camera camera, CameraViewModel viewModel)
        {
            _mainService.GetSynchronizationContext().Send(_ =>
            {
                viewModel.Status = camera.Status;
                viewModel.IdentifiedPeople.Clear();
                foreach(Person person in camera.IdentifiedPeople)
                {
                    viewModel.IdentifiedPeople.Add(person);
                }
            }, null);
        }

        public void LoadFloorList()
        {
            foreach (var floor in _floorService.GetFloorList())
            {
                _viewModel.Floors.Add(floor);
            }
        }

        public void BuildCommands()
        {
            _viewModel.FloorUpCommand = new RelayCommand(e =>
            {
                if (_viewModel.CurrentFloor < _viewModel.Floors.IndexOf(_viewModel.Floors.Last()))
                {
                    _viewModel.CurrentFloor++;
                    LoadFloor(_viewModel.Floors[_viewModel.CurrentFloor]);
                }
            });
            _viewModel.FloorDownCommand = new RelayCommand(e =>
            {
                if (_viewModel.CurrentFloor > 0)
                {
                    _viewModel.CurrentFloor--;
                    LoadFloor(_viewModel.Floors[_viewModel.CurrentFloor]);
                }
            });
            _viewModel.FloorListClickCommand = new RelayCommand(floor =>
            {
                Floor selectedFloor = floor as Floor;
                LoadFloor(selectedFloor as Floor);
                _viewModel.CurrentFloor = _viewModel.Floors.IndexOf(selectedFloor);
            });

            _viewModel.CameraClickCommand = new RelayCommand(o => {
                CameraViewModel RelatedCamera = o as CameraViewModel;
                _viewModel.SelectedCamera = RelatedCamera;
            });
        }
       
        //Loads floor to look at in home page
        public void LoadFloor(Floor newFloor)
        {
            if (newFloor == null)
                return;
            _viewModel.CurrentFloorImage = newFloor.FloorMap;
            _viewModel.FloorName = newFloor.Name;
            _viewModel.Cameras.Clear();
            foreach(var cam in newFloor.Cameras)
            {
                var cameraViewModel = new CameraViewModel { Status = cam.Status, PositionX = cam.PositionX, PositionY = cam.PositionY, Guid = cam.Guid };
                if (cam.IdentifiedPeople == null) cam.IdentifiedPeople = new List<Person>();
                foreach (var person in cam.IdentifiedPeople) cameraViewModel.IdentifiedPeople.Add(person);
                cameraViewModel.IdentifiedPeople.Add(new Person("t1", "t2"));
                _viewModel.Cameras.Add(cameraViewModel);
            }
        }
    }
}
