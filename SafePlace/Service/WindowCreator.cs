using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePlace.Models;
using SafePlace.Views.CameraWindowView;

namespace SafePlace.Service
{
    class WindowCreator : IWindowCreator
    {
        public void CreateCameraWindow(Camera camera)
        {
            var viewModel = new CameraWindowViewModel();
            var settingsPagePresenter = new CameraWindowPresenter(viewModel, camera);

            var view = new CameraWindow();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
