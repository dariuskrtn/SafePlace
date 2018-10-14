﻿using SafePlace.Views.HomePageView;
using SafePlace.Views.SettingsPageView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SafePlace.Service
{
    /*
     * Class responsible for different Page creation
     */

    class PageCreator : IPageCreator
    {
        private readonly IMainService _mainService;

        public PageCreator(IMainService mainService)
        {
            _mainService = mainService;
        }


        /*
         * Creates Home Page View and injects all required dependencies.
         */
        public Page CreateHomePage()
        {
            var homePageViewModel = new HomePageViewModel();
            var homePagePresenter = new HomePagePresenter(homePageViewModel, _mainService.GetLoggerInstance(), _mainService.GetSynchronizationContext());

            var homePageView = new HomePageView();
            homePageView.DataContext = homePageViewModel;

            return homePageView;
        }

        public Page CreateSettingsPage()
        {
            var settingsPageViewModel = new HomePageViewModel();
            var settingsPagePresenter = new HomePagePresenter(settingsPageViewModel, _mainService.GetLoggerInstance(), _mainService.GetSynchronizationContext());

            var settingsPageView = new SettingsPageView();
            settingsPageView.DataContext = settingsPageView;

            return settingsPageView;
        }
    }
}
