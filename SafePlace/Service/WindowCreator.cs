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
        private readonly IMainService _mainService;

        public WindowCreator(IMainService mainService)
        {
            _mainService = mainService;
        }
        public void CreateCameraWindow(Camera camera)
        {
            var viewModel = new CameraWindowViewModel();
            var presenter = new CameraWindowPresenter(viewModel, camera);

            var view = new CameraWindow();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
