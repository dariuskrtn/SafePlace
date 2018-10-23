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

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPagePresenter
    {
        //Name for an empty floor.
        private const string DefaultFloorName = "Input floor name here";

        private SettingsPageViewModel _viewModel;
        private readonly IMainService _mainService;
        private SynchronizationContext _synchronisationContext;
        private ILogger _logger;
        private IFloorService _floorService;
        private Floor _floor;
        private ICameraService _cameraService;
        
        //Camera we are currently working now with.
        private Camera _activeCamera;
        private string _newFloorName;
        public Floor NewFloor;

        //Following bools describing the status should be merged into a single enum. 
        //If _isCameraNew equals true, we're creating a camera. Else we're editing an existing one.
        private bool _isCameraNew;
        private bool _isEditModeOn = false;
        private bool _isAddFloorModeOn = false;
      //  private bool _is 

        public SettingsPagePresenter(SettingsPageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _newFloorName = DefaultFloorName;
            _mainService = mainService;
            _synchronisationContext = mainService.GetSynchronizationContext();
            GetServices(_mainService);
            BuildViewModel();
           
        }

        private void UploadFirstFloor()
        {
            _floor = _floorService.GetFloorList().FirstOrDefault();
            
            if (null != _floor)
                return;
            
            _floor = _floorService.CreateEmptyFloor(DefaultFloorName);
            _isAddFloorModeOn = true;
        }

        private void BuildViewModel()
        {
            UploadFirstFloor();
            BuildButttonsCommands();
            _viewModel.IsNewCameraShown = false;
            _viewModel.EditedCamera = _cameraService.CreateCamera(0, 0, false);
            ReloadCollection(_viewModel.FloorCollection, _floorService.GetFloorList().ToList());
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
            _viewModel.EditButtonClickCommand = new RelayCommand(e => EditButtonCommand(), e => { return NewFloor != null; });
            _viewModel.AddCameraButtonClickCommand = new RelayCommand(e => AddCameraButtonCommand(), e=> { return _viewModel.EditedCamera != null; });
            _viewModel.ChooseImageButtonClickCommand = new RelayCommand(e => ChooseImageButtonCommand());
            // Add/edit/save button.
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => FloorButtonCommand());
            _viewModel.CancelButtonClickCommand = new RelayCommand(e => CancelButtonCommand());
            _viewModel.DeleteButtonClickCommand = new RelayCommand(e => DeleteButtonCommand());
            _viewModel.FloorImageClickCommand = new RelayCommand(o => FloorImageClickCommand(o));
            _viewModel.FloorListClickCommand = new RelayCommand(o => FloorListClickCommand(o));
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
            //Can execute checks if newFloor is null.
            NewFloor = null;
            _isEditModeOn = true;
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
            if (true == _isEditModeOn)
            {
                UpdateFloorFromUI(_floor);
                _isEditModeOn = false;
            }
            else if (true == _isAddFloorModeOn)
            {
                UpdateFloorFromUI(_floor);
                _floorService.Add(_floor);
                _isAddFloorModeOn = false;
            }
            else
            {
                _floor = _floorService.CreateEmptyFloor(DefaultFloorName);
                _isAddFloorModeOn = true;
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
            floor.FloorName = _viewModel.FloorName;
            floor.FloorMap = _viewModel.FloorImage;
        }

        //Loads floor to look at in home page
        public void LoadFloor(Floor NewFloor)
        {
            if (NewFloor == null)
                return;
            _viewModel.FloorImage = NewFloor.FloorMap;
            ReloadCollection(_viewModel.CameraCollection, NewFloor.Cameras);
            _viewModel.FloorName = NewFloor.FloorName;
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
        private void FloorListClickCommand(object e)
        {
            if (!_isEditModeOn)
            {
                NewFloor = e as Floor;
                LoadFloor(NewFloor);
            }
        }
        // Mouse click on camera icon invekes this
        private void CameraIconClickCommand(object e)
        {
            Camera relatedCamera = e as Camera;
            SetPopUpFromCamera(relatedCamera);
            _isCameraNew = false;
            _viewModel.ShowPopUp = true;
            _viewModel.IsNewCameraShown = true;
            _activeCamera = relatedCamera;
            Camera cam = _cameraService.CreateCamera(relatedCamera.PositionX, relatedCamera.PositionY, false);
            cam.IPAddress = relatedCamera.IPAddress;
            cam.Name = relatedCamera.Name;
            _viewModel.EditedCamera = cam;
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
                _cameraService.AddCamera(_viewModel.EditedCamera);
                _floor.AddCamera(_viewModel.EditedCamera);
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
            //Camera newCamera = _cameraService.CreateCamera();
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
        ///x, y are click position coordinates in relation to the image.
            
        public void FloorImageClickCommand(Object click)
        {
            Point point = (Point)click;
            //Camera camera = _cameraService.CreateCamera();
            //properties have to be set to camera, before EditedCamera is set to camera.
            //camera.PositionX = (int)point.X;
            //camera.PositionY = (int)point.Y;
            //_viewModel.EditedCamera = camera;
            //Prievious 4 lines are equivalent to 1 after the comment.
            _viewModel.EditedCamera = _cameraService.CreateCamera((int)point.X, (int)point.Y, false);
            _viewModel.IsNewCameraShown = true;
            _logger.LogInfo($"Click detected at {point.X}, {point.Y}");
        }
    }
}
