using Microsoft.Win32;
using SafePlace.Service;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SafePlace.Views.SettingsPageView
{
    class SettingsPagePresenter
    {

        private SettingsPageViewModel _viewModel;
        private SynchronizationContext _synchronisationContext;
        private ILogger _logger;

        public SettingsPagePresenter(SettingsPageViewModel viewModel, ILogger logger, SynchronizationContext syncgronizationContext)
        {
            _viewModel = viewModel;
            _logger = logger;
            _synchronisationContext = syncgronizationContext;

            BuildViewModel();
        }

        private void BuildViewModel()
        {
            BuildButttonsCommans();
        }

        private void BuildButttonsCommans()
        {
            _viewModel.EditButtonClickCommand = new RelayCommand(e => OnEditButtonClicked());
            _viewModel.AddCameraButtonClickCommand = new RelayCommand(e => OnAddCameraButtonClicked());
            _viewModel.ChooseImageButtonClickCommand = new RelayCommand(e => OnChooseImageButtonClicked());
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => OnFloorButtonClicked());
            _viewModel.CancelButtonClickCommand = new RelayCommand(e => OnCancelButtonClicked());
            _viewModel.DeleteButtonClickCommand = new RelayCommand(e => OnDeleteButtonClicked());
        }

        #region Buttons Commands

        //Floor button - Add floor, Edit floor and Save (Depending on settings page state) 
        private void OnEditButtonClicked()
        {

        }

        private void OnAddCameraButtonClicked()
        {

        }

        // Choose and Change image is the same button
        private void OnChooseImageButtonClicked()
        {
            SelectFile();
        }

        private void OnFloorButtonClicked()
        {
            
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
            _viewModel.FloorImage = new BitmapImage(new Uri(openDialog.FileName));
        }
    }
}
