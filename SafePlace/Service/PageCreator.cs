using SafePlace.Views.HomePageView;
using SafePlace.Views.SettingsPageView;
using SafePlace.Views.UserRegistrationPageView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SafePlace.Service
{
    /*
     * Class responsible for different Page creation
     */

    class PageCreator : IPageCreator
    {
        private readonly ILogger _logger;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly IFloorService _floorService;
        private readonly IMainService _mainService;

        public PageCreator(ILogger logger, SynchronizationContext synchronizationContext, IFloorService floorService, IMainService mainService)
        {
            _logger = logger;
            _synchronizationContext = synchronizationContext;
            _floorService = floorService;
            _mainService = mainService;
        }


        /*
         * Creates Home Page View and injects all required dependencies.
         */
        public Page CreateHomePage()
        {
            var homePageViewModel = new HomePageViewModel();
            var homePagePresenter = new HomePagePresenter(homePageViewModel, _logger, _synchronizationContext, _floorService);

            var homePageView = new HomePageView();
            homePageView.DataContext = homePageViewModel;

            return homePageView;
        }

        public Page CreateSettingsPage()
        {
            var settingsPageViewModel = new SettingsPageViewModel();
            var settingsPagePresenter = new SettingsPagePresenter(settingsPageViewModel, _logger, _synchronizationContext);

            var settingsPageView = new SettingsPageView();
            settingsPageView.DataContext = settingsPageView;

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
