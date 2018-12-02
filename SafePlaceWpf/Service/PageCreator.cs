using SafePlace.Service;
using SafePlaceWpf.Views.HomePageView;
using SafePlaceWpf.Views.SettingsPageView;
using SafePlaceWpf.Views.UserRegistrationPageView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SafePlaceWpf.Service
{
    /*
     * Class responsible for different Page creation
     */

    class PageCreator : IPageCreator
    {
        private readonly IMainService _mainService;
        private readonly SynchronizationContext _synchronizationContext;

        public PageCreator(IMainService mainService, SynchronizationContext synchronizationContext)
        {
            _mainService = mainService;
            _synchronizationContext = synchronizationContext;
        }
        /*
         * Creates Home Page View and injects all required dependencies.
         */
        public Page CreateHomePage()
        {
            var homePageViewModel = new HomePageViewModel();
            var homePagePresenter = new HomePagePresenter(homePageViewModel, _mainService, _synchronizationContext);

            var homePageView = new HomePageView();
            homePageView.DataContext = homePageViewModel;

            return homePageView;
        }

        public Page CreateSettingsPage()
        {
            var settingsPageViewModel = new SettingsPageViewModel();
            var settingsPagePresenter = new SettingsPagePresenter(settingsPageViewModel, _mainService, _synchronizationContext);

            var settingsPageView = new SettingsPageView();
            settingsPageView.DataContext = settingsPageViewModel;

            return settingsPageView;
        }

        public Page CreateUserRegistrationPage()
        {
            var userRegistrationViewModel = new UserRegistrationPageViewModel();
            var userRegistrationPresenter = new UserRegistrationPagePresenter(userRegistrationViewModel, _mainService);

            var userRegistrationPageView = new UserRegistrationPageView();
            userRegistrationPageView.DataContext = userRegistrationViewModel;

            return userRegistrationPageView;
        }
    }
}
