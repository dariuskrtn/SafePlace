using SafePlace.Models;
using SafePlaceWpf.Service;
using SafePlaceWpf.Views.MainWindowView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SafePlaceWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dependencyInjector = new DependencyInjector();

            //Fake data from DB
            var floors = dependencyInjector.GetMainService().GetFloorServiceInstance().GetFloorList().ToArray();
            if (floors.Length < 2)
            {
                dependencyInjector.GetMainService().GetLoggerInstance().LogError("Error 404, fake data in DB not found.");
            }

            //A cycle through floors from db to change something if needed.
            foreach (var floor in floors)
            {

                if (floor.ImagePath != null)
                {
                    var img = new BitmapImage(new Uri(floor.ImagePath, UriKind.Relative));
                    //floor.FloorMap = new BitmapImage(new Uri(floor.ImagePath, UriKind.Relative));
                }
                //else floor.FloorMap = new BitmapImage(new Uri("/Images/no_image_icon.png", UriKind.Relative));
            }
            //int[] coords = {70, 56, 39, 594, 512, 550, 842, 550, 1148, 587, 1335, 33, 1066, 34, 864, 29, 387, 327, 771, 282}; 
            var cameras = dependencyInjector.GetMainService().GetCameraServiceInstance().GetAllCameras().ToArray();
            if (cameras.Length >= 3 && floors.Length >= 2)
            {
                foreach (Camera cam in cameras) cam.IdentifiedPeople = new List<Person>();
                floors[0].AddCamera(cameras[0]);
                floors[0].AddCamera(cameras[1]);
                floors[1].AddCamera(cameras[2]);
            }
            //var cam = mainService.GetCameraServiceInstance().CreateCamera();
            //cam.IPAddress = "http://192.168.8.101:8081/video";
            //cam.Name = "Main camera";
            //cam.PositionX = 70;
            //cam.PositionY = 56;
            //firstFloor.Cameras.Add(cam);
            //mainService.CreateCameraAnalyzeService(cam);

            //cam = mainService.GetCameraServiceInstance().CreateCamera();
            //cam.IPAddress = "http://192.168.8.101:8082/video";
            //cam.Name = "Main camera the second";
            //cam.PositionX = 39;
            //cam.PositionY = 594;
            //firstFloor.Cameras.Add(cam);
            //mainService.CreateCameraAnalyzeService(cam);

            //cam = mainService.GetCameraServiceInstance().CreateCamera();
            //cam.IPAddress = "http://192.168.8.101:8083/video";
            //cam.Name = "Main camera the third";
            //cam.PositionX = 100;
            //cam.PositionY = 100;
            //secondFloor.Cameras.Add(cam);
            //mainService.CreateCameraAnalyzeService(cam);

            //End of fake data

            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindowPresenter = new MainWindowPresenter(mainWindowViewModel, dependencyInjector.GetPageCreator());
            var mainWindow = new MainWindow();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
