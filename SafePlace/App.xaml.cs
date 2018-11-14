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

            //Fake data
            var floor = mainService.GetFloorServiceInstance().CreateFloor();
            floor.Name = "First floor";
            mainService.GetFloorServiceInstance().Add(floor);

            var secondFloor = mainService.GetFloorServiceInstance().CreateFloor("/Images/Floor2.png");
            secondFloor.Name = "Second floor";
            mainService.GetFloorServiceInstance().Add(secondFloor);
            /*
            int[] coords = {70, 56, 39, 594, 512, 550, 842, 550, 1148, 587, 1335, 33, 1066, 34, 864, 29, 387, 327, 771, 282}; 
            for (int i = 0; i < coords.Length; i += 2)
            {
                Camera newCamera = mainService.GetCameraServiceInstance().CreateCamera();
                newCamera.Name = coords[i] + "";
                newCamera.PositionX = coords[i];
                newCamera.PositionY = coords[i + 1];
                floor.Cameras.Add(newCamera);
            }
            /*for (int i = 0; i < 25; i++)
            {
                Camera newCamera = mainService.GetCameraServiceInstance().CreateCamera();
                //Setting camera position, which relates to the translation transformation of an image in its container.
                newCamera.PositionX = (3 - i % 5) * 200;
                newCamera.PositionY = (3 - i / 5) * 50;
               floor.Cameras.Add(newCamera);
            }*/
            var cam = mainService.GetCameraServiceInstance().CreateCamera();
            cam.IPAddress = "http://192.168.8.101:8081/video";
            cam.Name = "Main camera";
            cam.PositionX = 70;
            cam.PositionY = 56;
            floor.Cameras.Add(cam);
            mainService.CreateCameraAnalyzeService(cam);

            cam = mainService.GetCameraServiceInstance().CreateCamera();
            cam.IPAddress = "http://192.168.8.101:8082/video";
            cam.Name = "Main camera the second";
            cam.PositionX = 39;
            cam.PositionY = 594;
            floor.Cameras.Add(cam);
            mainService.CreateCameraAnalyzeService(cam);

            cam = mainService.GetCameraServiceInstance().CreateCamera();
            cam.IPAddress = "http://192.168.8.101:8083/video";
            cam.Name = "Main camera the third";
            cam.PositionX = 100;
            cam.PositionY = 100;
            secondFloor.Cameras.Add(cam);
            mainService.CreateCameraAnalyzeService(cam);
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
                    var name = "zzz";
                    var lastName = "yyy";
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
