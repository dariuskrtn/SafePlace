using SafePlace.Service;
using SafePlaceWpf.Service;
using SafePlaceWpf.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlaceWpf.Views.MainWindowView
{
    class MainWindowPresenter
    {
        private MainWindowViewModel _viewModel;
        private IPageCreator _pageCreator;
        

        public MainWindowPresenter(MainWindowViewModel viewModel, IPageCreator pageCreator)
        {
            _viewModel = viewModel;
            _pageCreator = pageCreator;

            BuildViewModel();
        }

        private void BuildViewModel()
        {
            _viewModel.DisplayPage = _pageCreator.CreateHomePage();
            _viewModel.HomePageCommand = new RelayCommand(e => OpenHomePageCommand());
            _viewModel.SettingsPageCommand = new RelayCommand(e => OpenSettingsPageCommand());
            _viewModel.UserRegistrationPageCommand = new RelayCommand(e => OpenUserRegistrationPageCommand());
            

        }

        private void OpenHomePageCommand()
        {
            _viewModel.DisplayPage = _pageCreator.CreateHomePage();
        }

        private void OpenSettingsPageCommand()
        {
            _viewModel.DisplayPage = _pageCreator.CreateSettingsPage();
        }
        private void OpenUserRegistrationPageCommand()
        {
            _viewModel.DisplayPage = _pageCreator.CreateUserRegistrationPage();
        }
    }
}
