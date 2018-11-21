using Microsoft.Win32;
using SafePlace.DB;
using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Views.MainWindowView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SafePlace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /*
         * Creates MainService and MainWindow to load and manage different views.
         */
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainService = new MainService();

            #region Fake data for DB in case we need to repopulate it.
            var firstFloor = mainService.GetFloorServiceInstance().CreateFloor();
            firstFloor.Name = "First floor";
            firstFloor.Guid = new Guid("3f692a4a-9952-4ac2-93b1-3ae34daa9b5f");
            var secondFloor = mainService.GetFloorServiceInstance().CreateFloor("/Images/Floor2.png");
            secondFloor.Name = "Second floor";
            secondFloor.Guid = new Guid("3f692a4a-9952-4ac2-93b1-3ae34daa9b5e");
            #endregion

            //Fake data from DB
            var floors = mainService.GetFloorServiceInstance().GetFloorList().ToArray();
            if(floors.Length <2)
            {
                mainService.GetLoggerInstance().LogError("Error 404, fake data in DB not found.");
            }
            
            //A cycle through floors from db to change something if needed.
            foreach(var floor in floors)
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
                foreach (Camera camer in cameras) camer.IdentifiedPeople = new List<Person>();
                floors[0].AddCamera(cameras[0]);
                floors[0].AddCamera(cameras[1]);
                floors[1].AddCamera(cameras[2]);
            }
            foreach (var camera in cameras)
            {
                mainService.CreateCameraAnalyzeService(camera);
            }

            #region DB data repopulation
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
            //mainService.GetFloorServiceInstance().Add(secondFloor);
            //mainService.GetFloorServiceInstance().Add(firstFloor);
            #endregion
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
                    var person = new Person {Guid = Guid, Name = name, LastName = lastName };
                    db.People.Add(person);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                mainService.GetLoggerInstance().LogError(ex.ToString());
                throw;
            }
            catch (Exception x) {
                mainService.GetLoggerInstance().LogError(x.ToString());
            }
        }
    }
}
