using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SafePlaceWpfWpf2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainService = new MainService();

            //Fake data from DB
            var floors = mainService.GetFloorServiceInstance().GetFloorList().ToArray();
            if (floors.Length < 2)
            {
                mainService.GetLoggerInstance().LogError("Error 404, fake data in DB not found.");
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
            var cameras = mainService.GetCameraServiceInstance().GetAllCameras().ToArray();
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
            var mainWindowPresenter = new MainWindowPresenter(mainWindowViewModel, mainService.GetPageCreatorInstance());
            var mainWindow = new MainWindow();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();


            // Connaction testing stuff, will remove when DB full implemented
            try
            {
                // Entity Framework testing
                using (var db = new DataContext())
                {
                    var name = "DefaultSchema";
                    var lastName = "Last";
                    Guid Guid = Guid.NewGuid();
                    var person = new Person { Guid = Guid, Name = name, LastName = lastName };
                    db.People.Add(person);
                    //db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                mainService.GetLoggerInstance().LogError(ex.ToString());
                throw;
            }
            catch (Exception x)
            {
                mainService.GetLoggerInstance().LogError(x.ToString());
            }
        }
    }
}
