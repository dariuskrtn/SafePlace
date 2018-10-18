using SafePlace.Models;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.UserRegistrationPageView
{
    class UserRegistrationPagePresenter
    {
        private readonly UserRegistrationPageViewModel _viewModel;
        public UserRegistrationPagePresenter(UserRegistrationPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BuildViewModel();
        }

        private void BuildViewModel()
        {
            _viewModel.RequiredImagesCount = 10;
            _viewModel.IsCapturing = true;

            _viewModel.RecordImageCommand = new RelayCommand(_ => SaveRecordCommand(), _ => SaveRecordCommandAllowed());
            _viewModel.SavePersonCommand = new RelayCommand(_ => SavePersonCommand(), _ => SavePersonCommandAllowed());
            _viewModel.ToggleCapturingCommand = new RelayCommand(_ => ToggleCapturingCommand());
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
            return !String.IsNullOrWhiteSpace(_viewModel.Name) &&
                   !String.IsNullOrWhiteSpace(_viewModel.Surname) &&
                   _viewModel.RequiredImagesCount == _viewModel.CurrentImagesCount;
        }
        private void SavePersonCommand()
        {
            var person = new Person();

        }
    }
}
