using Microsoft.Win32;
using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Views.MainWindowView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
            floor.FloorName = "First floor";
            for (int i = 0; i < 25; i++)
            {
                Camera newCamera = mainService.GetCameraServiceInstance().CreateCamera();
                //Setting camera position, which relates to the translation transformation of an image in its container.
                newCamera.PositionX = (3 - i % 5) * 200;
                newCamera.PositionY = (3 - i / 5) * 50;
               floor.Cameras.Add(newCamera);
            }
            //End of fake data


            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindowPresenter = new MainWindowPresenter(mainWindowViewModel, mainService.GetPageCreatorInstance());
            var mainWindow = new MainWindow();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
