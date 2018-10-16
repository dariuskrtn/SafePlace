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
            _viewModel.FloorButtonClickCommand = new RelayCommand(e => FloorButtonClickedCommand());
        }

        #region Commands

        private void FloorButtonClickedCommand()
        {
            SelectFile();
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
