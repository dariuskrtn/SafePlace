using AForge.Video;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.CameraWindowView
{
    class CameraWindowPresenter
    {
        private readonly CameraWindowViewModel _viewModel;
        private readonly Camera _camera;
        public CameraWindowPresenter(CameraWindowViewModel viewModel, Camera camera)
        {
            _viewModel = viewModel;
            _camera = camera;
            _viewModel.Stream = new MJPEGStream(_camera.IPAddress);
        }
    }
}
