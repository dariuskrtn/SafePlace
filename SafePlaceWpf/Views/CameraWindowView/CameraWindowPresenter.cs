using AForge.Video;
using SafePlace.Models;
using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceWpf.Views.CameraWindowView
{
    class CameraWindowPresenter
    {
        private readonly CameraWindowViewModel _viewModel;
        private readonly Camera _camera;
        public CameraWindowPresenter(CameraWindowViewModel viewModel, Camera camera)
        {
            _viewModel = viewModel;
            _camera = camera;
            //New MJPEGStream from IPAddress to get IP Camera frames.
            _viewModel.Stream = new MJPEGStream(_camera.IPAddress);
        }
    }
}
