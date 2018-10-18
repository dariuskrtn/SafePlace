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

namespace SafePlace.Views.HomePageView
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

        private IList<Floor> _floors;

        private string[] Names = { "Joseph", "Johan", "John", "Jack", "Joe", "Johnattan", "Jacob", "Jason", "Jennifer", "Jay" };
        private string[] LastNames = { "Peugeot", "Ferrari", "Harrari", "Smith", "Sans", "Rutherford", "Boore", "Huxley", "Jacksondaughter", "Joestar"};
        private Random Random;

        public HomePagePresenter(HomePageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _mainService = mainService;
            GetServices(_mainService);
            LoadFloorList();
            BuildViewModel();
        }

        private void GetServices(IMainService mainService)
        {
            _logger = mainService.GetLoggerInstance();
            _synchronizationContext = mainService.GetSynchronizationContext();
            _floorService = mainService.GetFloorServiceInstance();
        }

        private void BuildViewModel()
        {
            //Setting the default starting floor's image.
            ReloadObservableCollection(_viewModel.Floors, _floorService.GetFloorList().ToList());
            _viewModel.CurrentFloorImage = _floors[0].FloorMap;
            _viewModel.CurrentFloor = 0;
            _viewModel.CameraImage = new BitmapImage(new Uri("/Images/camera.png", UriKind.Relative));
            //Random number generator for random data generating
            Random = new Random();
            foreach (Floor floor in _floors)
            {
                _viewModel.FloorList.Add(floor.FloorName);
            }
            
            Image floorImage = new Image()
            {
                Source = _viewModel.CurrentFloorImage
            };

            foreach (Camera cam in _floors[0].Cameras)
            {
                _viewModel.Cameras.Add(cam);
            }

            _viewModel.FloorUpCommand = new RelayCommand(e =>
            {
                if (_viewModel.CurrentFloor < _floors.IndexOf(_floors.Last()))
                {
                    _viewModel.CurrentFloor++;
                    LoadFloor(_floors[_viewModel.CurrentFloor]);
                }
            });
            _viewModel.FloorDownCommand = new RelayCommand(e =>
            {
                if (_viewModel.CurrentFloor > 0)
                {
                    _viewModel.CurrentFloor--;
                    LoadFloor(_floors[_viewModel.CurrentFloor]);
                }
            });
            
            _viewModel.CameraClickCommand = new RelayCommand(o => {
                Camera RelatedCamera = o as Camera;
                //Adding more dummy data
                RelatedCamera.IdentifiedPeople.Add(new Person() { Name = Names[Random.Next(0, 9)], LastName = LastNames[Random.Next(0, 9)]});
                //If we create a new observable list from the IdentifiedPeople list, the link between UIElement ItemControl and the list will be destroyed.
                //So currently we reload it with new items
                ReloadObservableCollection(_viewModel.SpottedPeople, RelatedCamera.IdentifiedPeople);
                _logger.LogInfo($"You clicked on camera with the Guid of: {RelatedCamera.Guid.ToString()}");
                  
            });
        }

        private void LoadFloorList()
        {
            _floors = _floorService.GetFloorList().ToList();
        }
        ///A method fit for edit page, allows creating images with accurate position floor-plan-wise when put in an observable collection.
        ///Would be more accurate if ImageWidth and imageHeight are calculated from the current floor image.
        ///x, y are click position coordinates
        ///The image must be shown in the
        public Image ImageFromCoords(int x, int y)
        {
            TransformGroup group = new TransformGroup();
            group.Children.Add(new TranslateTransform() { X = x, Y = y});
            Image newImage = new Image()
            {
                RenderTransform = group,
                Uid = Guid.NewGuid().ToString(),
                Height = 30,
                Source = _viewModel.CameraImage
            };
            newImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newImage.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            return newImage;
        }

        public void LoadFloor(Floor NewFloor)
        {
            _viewModel.CurrentFloorImage = NewFloor.FloorMap;
            ReloadObservableCollection(_viewModel.Cameras, NewFloor.Cameras);
            _viewModel.SpottedPeople.Clear();
        }

        public void ReloadObservableCollection<T>(ObservableCollection<T> observableColl, IList<T> list)
        {
            observableColl.Clear();
            list.ToList().ForEach(o => observableColl.Add(o));
        }
    }
}
