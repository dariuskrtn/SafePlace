using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.SettingsPageView.CameraAddPopUp
{
    class CameraAddPopUpPresenter
    {
        private CameraAddPopUpViewModel _viewModel;
        private readonly ICameraService _cameraService;
        private Floor _currentFloor;
        public CameraAddPopUpPresenter(CameraAddPopUpViewModel viewModel, IMainService mainService, Floor floor)
        {
            _viewModel = viewModel;
            _cameraService = mainService.GetCameraServiceInstance();
            _currentFloor = floor;
        }

        public void BuildViewModel(int cameraX, int cameraY)
        {
            _viewModel = new CameraAddPopUpViewModel()
            {
                PositionX = cameraX,
                PositionY = cameraY
            };
            _viewModel.CameraAddCommand = new RelayCommand(e => OnAddCameraButtonClicked());
        }

        public void OnAddCameraButtonClicked()
        {
            _currentFloor.AddCamera(CameraFromPopUp());
        }
        public Camera CameraFromPopUp()
        {
            Camera newCamera =_cameraService.CreateCamera();
            newCamera.Name = _viewModel.Name;
            newCamera.PositionX = _viewModel.PositionX;
            newCamera.PositionY = _viewModel.PositionY;
            newCamera.IPAddress = _viewModel.IPAdress;
            return newCamera;
        }
    }
}
