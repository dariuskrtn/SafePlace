using Microsoft.Win32;
using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Utilities;
using SafePlace.Views.SettingsPageView.CameraAddPopUp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPagePresenter
    {

        private SettingsPageViewModel _viewModel;
        private readonly IMainService _mainService;
        private SynchronizationContext _synchronisationContext;
        private ILogger _logger;
        private IFloorService _floorService;
        private Floor _floor;
        private readonly Window _cameraAddPopUpView;
        private ICameraService _cameraService;

        public SettingsPagePresenter(SettingsPageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _mainService = mainService;
            GetServices(_mainService);
            BuildViewModel();
            _cameraAddPopUpView = _mainService.GetPageCreatorInstance().CreateCameraAddPopUp();
        }

        private void BuildViewModel()
        {
            BuildButttonsCommands();

            // New floor later will be created when pressed addFloor button, for now this button do not exist
            _floor = _floorService.CreateEmptyFloor();
            _viewModel.FloorImage = _floor.FloorMap;
            _viewModel.FloorName = _floor.FloorName;
        }

        private void GetServices(IMainService mainService)
        {
            _logger = mainService.GetLoggerInstance();
            _floorService = mainService.GetFloorServiceInstance();
            _cameraService = mainService.GetCameraServiceInstance();
        }

        private void BuildButttonsCommands()
        {
            _viewModel.EditButtonClickCommand = new RelayCommand(e => OnEditButtonClicked());
            _viewModel.AddCameraButtonClickCommand = new RelayCommand(e => OnAddCameraButtonClicked());
            _viewModel.ChooseImageButtonClickCommand = new RelayCommand(e => OnChooseImageButtonClicked());
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => OnFloorButtonClicked());
            _viewModel.CancelButtonClickCommand = new RelayCommand(e => OnCancelButtonClicked());
            _viewModel.DeleteButtonClickCommand = new RelayCommand(e => OnDeleteButtonClicked());
            _viewModel.FloorImageClickCommand = new RelayCommand(o => OnFloorImageClicked(o));

            _viewModel.CameraAddCommand = new RelayCommand(e => PopUp_OnComfirmButtonClicked());
            _viewModel.CameraCancelCommand = new RelayCommand(e => PopUp_OnCancelButtonClicked());
        }

        //-------------------------------------------------------------
        // Main settings window code
        //------------------------------------------------------------

        #region Buttons Commands

        private void OnEditButtonClicked()
        {

        }

        private void OnAddCameraButtonClicked()
        {
            _viewModel.ShowPopUp = true;
        }

        // Choose and Change image is the same button
        private void OnChooseImageButtonClicked()
        {
            SelectFile();
        }

        //Floor button - Add floor, Edit floor and Save (Depending on settings page state) 
        // Consider this button as "save button" for now
        private void OnFloorButtonClicked()
        {
            _floor.FloorName = _viewModel.FloorName;
        }

        // CAncel and Restore is the same button
        private void OnCancelButtonClicked()
        {
            
        }

        private void OnDeleteButtonClicked()
        {
            
        }

        #endregion

        //Open file dialog, select image and assign to _viewModel
        private void SelectFile()
        {
            // Open file dialog   
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open Image";
            // Image filters  
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (openDialog.ShowDialog() == false)
            {
                // Maybe should throw exception and show message for user?
                _logger.LogInfo(" Failed to open selected image");
                return;
            }

            updateFloorImage(openDialog.FileName);
        }

        // updates image both in floor object as well as on screen
        private void updateFloorImage(String imagePath)
        {
            _floor.FloorMap = new BitmapImage(new Uri(imagePath));
            _viewModel.FloorImage = _floor.FloorMap;
        }


        //-------------------------------------------------------------
        // Pop-up code
        //------------------------------------------------------------

        #region Buttons

        private void PopUp_OnComfirmButtonClicked()
        {
            _floor.AddCamera(CameraFromPopUp());
            _viewModel.ShowPopUp = false;
            ClearPopUp();
        }
        private void PopUp_OnCancelButtonClicked()
        {
            _viewModel.ShowPopUp = false;
            ClearPopUp();
        }

        #endregion

        public Camera CameraFromPopUp()
        {
            Camera newCamera = _cameraService.CreateCamera();
            newCamera.Name = _viewModel.Name;
            newCamera.PositionX = _viewModel.PositionX;
            newCamera.PositionY = _viewModel.PositionY;
            newCamera.IPAddress = _viewModel.IPAdress;
            return newCamera;
        }
        public void ClearPopUp()
        {
            _viewModel.Name = _viewModel.IPAdress = "";
            _viewModel.PositionX = _viewModel.PositionY = 0;
        }
        ///x, y are click position coordinates in relation to the image.
            
        public void OnFloorImageClicked(Object click)
        {

        }
    }
}
