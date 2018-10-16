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
                //If the clicked image is a floor plan, then add a new camera on click position.
                //This code fragment is meant to be here temporarily, unless we decide to allow camera creation in the home page.
                if (ClickedImage.Name == "FloorMap")
                {
                    var clickPosition = args.GetPosition(ClickedImage);
                    ///Seems to work, but also be faulty as a warning appears System.Windows.Data Error: 26 : ItemTemplate..."
                    _viewModel.Cameras.Add(ImageFromCoords((int)clickPosition.X, (int)clickPosition.Y, ClickedImage));
                }
            });
        }

        private void LoadFloorList()
        {
            _floors = _floorService.GetFloorList().ToList();
        }
        ///A method fit for edit page, allows creating images with accurate position floor-plan-wise when put in an observable collection.
        ///Would be more accurate if ImageWidth and imageHeight are calculated from the current floor image.
        public Image ImageFromCoords(int x, int y, Image floorImage)
        {
            TransformGroup group = new TransformGroup();
            int ImageWidth = (int)floorImage.ActualWidth, ImageHeight = (int)floorImage.ActualHeight;
            group.Children.Add(new TranslateTransform() { X = x - ImageWidth/2, Y = y - ImageHeight/2});
            Image newImage = new Image()
            {
                RenderTransform = group,
                Uid = Guid.NewGuid().ToString(),
                Height = 30,
                Source = _viewModel.CameraImage
            };
            return newImage;
        }

    }
}
