using Microsoft.Win32;
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
using System.Text.RegularExpressions;
using SafePlace.Enums;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPagePresenter
    {
        //Name for an empty floor.
        private const string DefaultFloorName = "Input floor name here";

        private readonly IMainService _mainService;
        private SettingsPageViewModel _viewModel; 
        private SynchronizationContext _synchronisationContext;
        private ILogger _logger;
        private IFloorService _floorService;
        private ICameraService _cameraService;
        private Floor _floor;
        //Camera we are currently working now with.
        private Camera _activeCamera;
        //If _isCameraNew equals true, we're creating a camera. Else we're editing an existing one.
        private bool _isCameraNew;

        private SettingsModes currentMode;
        private SettingsModes _currentMode
        {
            set
            {
                currentMode = value;
                _viewModel.SettingsModes = value;
            }
            get
            {
                return currentMode;
            }
        }
        
        public SettingsPagePresenter(SettingsPageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _mainService = mainService;
            _synchronisationContext = mainService.GetSynchronizationContext();
            _currentMode = SettingsModes.Preview;
            GetServices(_mainService);
            BuildViewModel();

        }

        private void UploadFirstFloor()
        {
            _floor = _floorService.GetFloorList().FirstOrDefault();
            ReloadCollection(_viewModel.CameraCollection, _floor.Cameras);
            
            if (null != _floor)
            {
                return;
            }
            
            _floor = _floorService.CreateEmptyFloor(DefaultFloorName);
            _currentMode = SettingsModes.CreatingNew;
        }

        private void BuildViewModel()
        {
            UploadFirstFloor();
            BuildButttonsCommands();
            _viewModel.IsNewCameraShown = false;
            _viewModel.EditedCamera = _cameraService.CreateCamera(false, 0, 0);
            ReloadCollection(_viewModel.FloorCollection, _floorService.GetFloorList().ToList());
            _viewModel.FloorImage = _floor.FloorMap;
            _viewModel.FloorName = _floor.Name;
        }

        private void GetServices(IMainService mainService)
        {
            _logger = mainService.GetLoggerInstance();
            _floorService = mainService.GetFloorServiceInstance();
            _cameraService = mainService.GetCameraServiceInstance();
        }

        private void BuildButttonsCommands()
        {
            _viewModel.EditButtonClickCommand = new RelayCommand(e => EditButtonCommand(), e => {return _currentMode == SettingsModes.Preview; });
            // _current mode havo to be either editing or creating new floor
            _viewModel.AddCameraButtonClickCommand = new RelayCommand(e => AddCameraButtonCommand(), e=> { return _viewModel.EditedCamera != null && (_currentMode == SettingsModes.CreatingNew || _currentMode == SettingsModes.Editing); });
            _viewModel.ChooseImageButtonClickCommand = new RelayCommand(e => ChooseImageButtonCommand(), e=> { return _currentMode == SettingsModes.CreatingNew || _currentMode == SettingsModes.Editing; });
            // AddFloor/save button
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => FloorButtonCommand());
            _viewModel.CancelButtonClickCommand = new RelayCommand(e => CancelButtonCommand(), e => { return _currentMode == SettingsModes.CreatingNew || _currentMode == SettingsModes.Editing; });
            _viewModel.DeleteButtonClickCommand = new RelayCommand(e => DeleteButtonCommand(), e => {return _currentMode == SettingsModes.Editing; });
            _viewModel.FloorImageClickCommand = new RelayCommand(o => FloorImageClickCommand(o));
            _viewModel.FloorListClickCommand = new RelayCommand(o => FloorListClickCommand(o));
            _viewModel.CameraClickCommand = new RelayCommand(e => CameraIconClickCommand(e));

            _viewModel.CameraAddCommand = new RelayCommand(e => PopUp_ComfirmButtonCommand(), e => { return _currentMode == SettingsModes.CreatingNew || _currentMode == SettingsModes.Editing; });
            _viewModel.CameraCancelCommand = new RelayCommand(e => PopUp_CancelButtonCommand());
        }

        //-------------------------------------------------------------
        // Main settings window code
        //------------------------------------------------------------



        #region Buttons Commands

        private void EditButtonCommand()
        {
            //Can execute checks if newFloor is null.
            _currentMode = SettingsModes.Editing;
            //_viewModel.IsEditModeOff = false;
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
            if (SettingsModes.Editing == _currentMode)
            {
                if (false == CheckIfNameIsValid())
                    return;
                UpdateFloorFromUI(_floor);
                _currentMode = SettingsModes.Preview;
            }
            else if (SettingsModes.CreatingNew == _currentMode)
            {
                if (false == CheckIfNameIsValid())
                    return;
                UpdateFloorFromUI(_floor);
                _floorService.Add(_floor);
                _currentMode = SettingsModes.Preview;
                ReloadCollection(_viewModel.FloorCollection, _floorService.GetFloorList().ToList());
            }
            else
            {
                _currentMode = SettingsModes.CreatingNew;
                _floor = _floorService.CreateEmptyFloor(DefaultFloorName);
                LoadFloor(_floor);
            }

        }

        // CAncel and Restore is the same button
        private void CancelButtonCommand()
        {
            
        }

        private void DeleteButtonCommand()
        {
            
        }

        #endregion

        private void UpdateFloorFromUI(Floor floor)
        {
            floor.Name = _viewModel.FloorName;
            floor.FloorMap = _viewModel.FloorImage;
        }

        //Loads floor to look at in home page
        public void LoadFloor(Floor floor)
        {
            if (floor == null)
                return;
            _floor = floor;
            _viewModel.FloorImage = _floor.FloorMap;
            ReloadCollection(_viewModel.CameraCollection, _floor.Cameras);
            _viewModel.FloorName = _floor.Name;
        }
        //Open file dialog, select image and assign to _viewModel
        private void SelectFile()
        {
            // Open file dialog   
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open Image";
            // Image filters  
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png;";

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
        //We get a floor item from the list and load it if we're not editing.
        private void FloorListClickCommand(object e)
        {
            // Change floors possible only when in preview mode
            if (SettingsModes.Preview == _currentMode)
            {
                Floor floor = e as Floor;
                LoadFloor(floor);
            }
        }
        // Mouse click on camera icon invekes this
        private void CameraIconClickCommand(object e)
        {
            Camera relatedCamera = e as Camera;
            SetPopUpFromCamera(relatedCamera);
            _isCameraNew = false;
            _viewModel.ShowPopUp = true;
            _viewModel.IsNewCameraShown = _currentMode != SettingsModes.Preview;
            _activeCamera = relatedCamera;
            Camera cam = _cameraService.CreateCamera(false, relatedCamera.PositionX, relatedCamera.PositionY);
            cam.IPAddress = relatedCamera.IPAddress;
            cam.Name = relatedCamera.Name;
            _viewModel.EditedCamera = cam;
        }

        public void ReloadCollection<T>(ObservableCollection<T> observableColl, ICollection<T> list)
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
                _cameraService.AddCamera(_viewModel.EditedCamera);
                _floor.AddCamera(_viewModel.EditedCamera);
                var communicator = new DBCommunication.DBCommunicator();
                communicator.AddCamera(_viewModel.EditedCamera);
                _viewModel.CameraCollection.Add(_viewModel.EditedCamera);
            }
            else
            {

                Camera cameraInstance = _floor.Cameras.GetFloorCamera(_activeCamera.Guid);
                UpdateCameraFromUI(cameraInstance);
                ReloadCollection(_viewModel.CameraCollection, _floor.Cameras);
            }
            _viewModel.EditedCamera = null;
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
            camera.PositionY = _viewModel.EditedCamera.PositionY;
            camera.PositionX = _viewModel.EditedCamera.PositionX;
            camera.Name = _viewModel.EditedCamera.Name;
            camera.IPAddress = _viewModel.EditedCamera.IPAddress;
        }

        private void SetPopUpFromCamera(Camera camera)
        {
            _viewModel.EditedCamera = camera;
        }

        private void ClearPopUp()
        {
            _viewModel.EditedCamera = null;
        }
        

        private bool CheckIfNameIsValid()
        {
            bool isValid = Regex.IsMatch(_viewModel.FloorName, @"^[a-zA-Z_ ]+\d*$");
            if (true == isValid)
            {
                _viewModel.InvalidNameNotification = "";
            }
            else _viewModel.InvalidNameNotification = "Please input valid name!";
            return isValid;
        }

        ///x, y are click position coordinates in relation to the image.
        public void FloorImageClickCommand(Object click)
        {
            // We cant add cameras when in preview mode
            if (SettingsModes.Preview == _currentMode)
                return;

            Point point = (Point)click;
            //Camera camera = _cameraService.CreateCamera();
            //properties have to be set to camera, before EditedCamera is set to camera.
            //camera.PositionX = (int)point.X;
            //camera.PositionY = (int)point.Y;
            //_viewModel.EditedCamera = camera;
            //Prievious 4 lines are equivalent to 1 after the comment.
            _viewModel.EditedCamera = _cameraService.CreateCamera(false, (int)point.X, (int)point.Y);
            _viewModel.IsNewCameraShown = true;
            _logger.LogInfo($"Click detected at {point.X}, {point.Y}");
        }


    }
}
