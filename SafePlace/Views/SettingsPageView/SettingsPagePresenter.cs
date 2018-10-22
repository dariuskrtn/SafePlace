﻿using Microsoft.Win32;
using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Utilities;
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
using System.Collections.ObjectModel;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPagePresenter
    {
        //Name for an empty floor.
        private const string _defaultFloorName = "Input floor name here";

        private SettingsPageViewModel _viewModel;
        private readonly IMainService _mainService;
        private SynchronizationContext _synchronisationContext;
        private ILogger _logger;
        private IFloorService _floorService;
        private Floor _floor;
        private ICameraService _cameraService;
        //If _isCameraNew equals true, we're creating a camera. Else we're editing an existing one.
        private bool _isCameraNew;
        //Camera we are currently working now with.
        private Camera _activeCamera;
        private string _newFloorName;

        public SettingsPagePresenter(SettingsPageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _newFloorName = _defaultFloorName;
            _mainService = mainService;
            _synchronisationContext = mainService.GetSynchronizationContext();
            GetServices(_mainService);
            BuildViewModel();
        }

        private void BuildViewModel()
        {
            BuildButttonsCommands();
            _viewModel.IsNewCameraShown = false;
            // New floor later will be created when pressed addFloor button, for now this button do not exist
            _floor = _floorService.CreateEmptyFloor(_newFloorName);
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
            _viewModel.EditButtonClickCommand = new RelayCommand(e => EditButtonCommand());
            _viewModel.AddCameraButtonClickCommand = new RelayCommand(e => AddCameraButtonCommand());
            _viewModel.ChooseImageButtonClickCommand = new RelayCommand(e => ChooseImageButtonCommand());
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => FloorButtonCommand());
            _viewModel.CancelButtonClickCommand = new RelayCommand(e => CancelButtonCommand());
            _viewModel.DeleteButtonClickCommand = new RelayCommand(e => DeleteButtonCommand());
            _viewModel.FloorImageClickCommand = new RelayCommand(o => FloorImageClickCommand(o));

            _viewModel.CameraClickCommand = new RelayCommand(e => CameraIconClickCommand(e));

            _viewModel.CameraAddCommand = new RelayCommand(e => PopUp_ComfirmButtonCommand());
            _viewModel.CameraCancelCommand = new RelayCommand(e => PopUp_CancelButtonCommand());
        }

        //-------------------------------------------------------------
        // Main settings window code
        //------------------------------------------------------------



        #region Buttons Commands

        private void EditButtonCommand()
        {

        }

        private void AddCameraButtonCommand()
        {
            _isCameraNew = true;
            _viewModel.ShowPopUp = true;
        }

        // Choose and Change image is the same button
        private void ChooseImageButtonCommand()
        {
            SelectFile();
        }

        //Floor button - Add floor, Edit floor and Save (Depending on settings page state) 
        // Consider this button as "save button" for now
        private void FloorButtonCommand()
        {
            _floor.FloorName = _viewModel.FloorName;
        }

        // CAncel and Restore is the same button
        private void CancelButtonCommand()
        {
            
        }

        private void DeleteButtonCommand()
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

            UpdateFloorImage(openDialog.FileName);
        }

        // updates image both in floor object as well as on screen
        private void UpdateFloorImage(String imagePath)
        {
            _floor.FloorMap = new BitmapImage(new Uri(imagePath));
            _viewModel.FloorImage = _floor.FloorMap;
        }

        // Mouse click on camera icon invekes this
        private void CameraIconClickCommand(object e)
        {
            Camera relatedCamera = e as Camera;
            SetPopUpFromCamera(relatedCamera);
            _isCameraNew = false;
            _viewModel.ShowPopUp = true;
            _activeCamera = relatedCamera;
        }

        public void ReloadCollection<T>(ObservableCollection<T> observableColl, IList<T> list)
        {
            observableColl.Clear();
            list.ToList().ForEach(o => observableColl.Add(o));
        }

        //-------------------------------------------------------------
        // Pop-up code
        //------------------------------------------------------------

        #region Buttons

        private void PopUp_ComfirmButtonCommand()
        {
            if (_isCameraNew)
            {
                Camera newCamera = _cameraService.CreateCamera();
                UpdateCameraFromUI(newCamera);
                _floor.AddCamera(newCamera);
                _viewModel.CameraCollection.Add(newCamera);
            }
            else
            {
                Camera cameraInstance = _floor.Cameras.GetFloorCamera(_activeCamera.Guid);
                UpdateCameraFromUI(cameraInstance);
                ReloadCollection(_viewModel.CameraCollection, _floor.Cameras);
            }
            
            _viewModel.ShowPopUp = false;
            _viewModel.IsNewCameraShown = false;
            ClearPopUp();
        }
        private void PopUp_CancelButtonCommand()
        {
            _viewModel.ShowPopUp = false;
            _viewModel.IsNewCameraShown = false;
            ClearPopUp();
        }

        #endregion

        
        private void UpdateCameraFromUI(Camera camera)
        {
            //Camera newCamera = _cameraService.CreateCamera();
            camera.Name = _viewModel.CameraName;
            camera.IPAddress = _viewModel.IPAddress;
            camera.PositionX = _viewModel.PositionX;
            camera.PositionY = _viewModel.PositionY;
        }

        private void SetPopUpFromCamera(Camera camera)
        {
            _viewModel.CameraName = camera.Name;
            _viewModel.IPAddress = camera.IPAddress;
            _viewModel.PositionX = camera.PositionX;
            _viewModel.PositionY = camera.PositionY;
        }

        private void ClearPopUp()
        {
            _viewModel.CameraName = _viewModel.IPAddress = "";
            _viewModel.PositionX = _viewModel.PositionY = 0;
        }
        ///x, y are click position coordinates in relation to the image.
            
        public void FloorImageClickCommand(Object click)
        {
            Point point = (Point)click;
            _viewModel.IsNewCameraShown = true;
            _viewModel.PositionX = (int)point.X;
            _viewModel.PositionY = (int)point.Y;
            _logger.LogInfo($"Click detected at {point.X}, {point.Y}");
        }
    }
}
