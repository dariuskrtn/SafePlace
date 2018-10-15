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

namespace SafePlace.Views.HomePageView
{
    /*
     * Moved Home Page from MainWindow.
     * Please check if all dependencies are required and remove/add the ones which are needed.
     */
    class HomePagePresenter
    {
        private HomePageViewModel _viewModel;
        private SynchronizationContext _synchronizationContext;
        private ILogger _logger;
        private IFloorService _floorService;

        private IList<Floor> _floors;

        public HomePagePresenter(HomePageViewModel viewModel, ILogger logger, SynchronizationContext synchronizationContext, IFloorService floorService)
        {
            _viewModel = viewModel;
            _logger = logger;
            _synchronizationContext = synchronizationContext;
            _floorService = floorService;
            LoadFloorList();
            BuildViewModel();
        }

        private void BuildViewModel()
        {
            //Setting the default starting floor's image.
            _viewModel.CurrentFloorImage = _floors[0].FloorMap;
            _viewModel.CameraImage = new BitmapImage(new Uri("/Images/camera.png", UriKind.Relative));
            foreach (Floor floor in _floors)
            {
                _viewModel.FloorList.Add(floor.FloorName);
            }

            //Initializing Cameras observable collection, which will be used to store names, connecting images to cameras
            _viewModel.Cameras = new System.Collections.ObjectModel.ObservableCollection<Image>();
            
            foreach (Camera cam in _floors[0].Cameras)
            {
                TransformGroup group = new TransformGroup();
                group.Children.Add(new TranslateTransform() {X = cam.PositionX, Y = cam.PositionY });
                Image newImage = new Image()
                {
                    RenderTransform = group,
                    Uid = cam.guid.ToString(),
                    Height = 30,
                    Source = _viewModel.CameraImage
                };
                _viewModel.Cameras.Add(newImage);
            }

         
            
            _viewModel.CameraClickCommand = new RelayCommand(o => {
                MouseButtonEventArgs args = (MouseButtonEventArgs)o;
                Image ClickedImage = args.Source as Image;
                _logger.LogInfo($"Camera click detected. Sender is of type:{ClickedImage}");
                Console.WriteLine($"Coordinates of the click: {args.GetPosition(ClickedImage)}");
                Image img = new Image();
                img.Margin = new System.Windows.Thickness();
            });

        }

        private void LoadFloorList()
        {
            _floors = _floorService.GetFloorList().ToList();
        }


    }
}
