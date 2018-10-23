using SafePlace.Models;
using SafePlace.Service;
using SafePlace.Utilities;
using SafePlace.WpfComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Views.UserRegistrationPageView
{
    class UserRegistrationPagePresenter
    {
        private readonly UserRegistrationPageViewModel _viewModel;
        private readonly IMainService _mainService;
        private readonly ICameraService _cameraService;
        private readonly IPersonService _personService;
        private readonly IFaceRecognitionService _recognitionService;
        public UserRegistrationPagePresenter(UserRegistrationPageViewModel viewModel, IMainService mainService)
        {
            _viewModel = viewModel;
            _mainService = mainService;
            _cameraService = mainService.GetCameraServiceInstance();
            _personService = mainService.GetPersonServiceInstance();
            _recognitionService = mainService.GetFaceRecognitionServiceInstance();

            BuildViewModel();
        }

        private void BuildViewModel()
        {
            _viewModel.RequiredImagesCount = 10;
            _viewModel.IsCapturing = true;
            _viewModel.IsSaving = false;

            _viewModel.RecordImageCommand = new RelayCommand(_ => SaveRecordCommand(), _ => SaveRecordCommandAllowed());
            _viewModel.SavePersonCommand = new RelayCommand(_ => SavePersonCommand(), _ => SavePersonCommandAllowed());
            _viewModel.ToggleCapturingCommand = new RelayCommand(_ => ToggleCapturingCommand());

            PopulateCameras();
        }

        private void PopulateCameras()
        {
            //Get all registered cameras and add them to combobox for multiple selection.
            foreach (Camera cam in _cameraService.GetAllCameras())
            {
                _viewModel.Cameras.Add(new MultiComboboxItem<Camera>() { Item = cam, Name = cam.Name, SelectedItems = _viewModel.SelectedCameras });
            }
        }

        private void ToggleCapturingCommand()
        {
            _viewModel.IsCapturing = !_viewModel.IsCapturing;
        }

        private bool SaveRecordCommandAllowed()
        {
            return _viewModel.IsCapturing && _viewModel.CurrentImagesCount < _viewModel.RequiredImagesCount;
        }
        private void SaveRecordCommand()
        {
            _viewModel.IsRecording = true;
            _viewModel.CurrentImagesCount++;
        }

        private bool SavePersonCommandAllowed()
        {
            return !_viewModel.IsSaving &&
                   !String.IsNullOrWhiteSpace(_viewModel.Name) &&
                   !String.IsNullOrWhiteSpace(_viewModel.LastName) &&
                   _viewModel.RequiredImagesCount == _viewModel.CurrentImagesCount;
        }
        private void SavePersonCommand()
        {
            var person = _personService.CreatePerson();
            person.Name = _viewModel.Name;
            person.LastName = _viewModel.LastName;
            person.AllowedCameras = new Collection<Guid>();
            foreach (var cam in _viewModel.SelectedCameras)
            {
                person.AllowedCameras.Add(cam.Guid);
            }

            new Thread(_ => RegisterPersonFace(person)).Start();
        }

        //Async call to face recognition service to save new persons face into the database.
        private async void RegisterPersonFace(Person person)
        {
            _viewModel.IsSaving = true;

            await _recognitionService.RegisterPerson(person.Guid.ToString(), _viewModel.Recordings);

            await _recognitionService.TrainAI();

            _viewModel.IsSaving = false;
        }
    }
}
