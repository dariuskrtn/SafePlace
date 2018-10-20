﻿using SafePlace.Views.HomePageView;
using SafePlace.Views.SettingsPageView;
using SafePlace.Views.SettingsPageView.CameraAddPopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
            var homePagePresenter = new HomePagePresenter(homePageViewModel, _mainService);

            var homePageView = new HomePageView();
            homePageView.DataContext = homePageViewModel;

            return homePageView;
        }

        public Page CreateSettingsPage()
        {
            var settingsPageViewModel = new SettingsPageViewModel();
            var settingsPagePresenter = new SettingsPagePresenter(settingsPageViewModel, _mainService);

            var settingsPageView = new SettingsPageView();
            settingsPageView.DataContext = settingsPageViewModel;

            return settingsPageView;
        }

        public Window CreateCameraAddPopUp()
        {
            var cameraAddPopUpViewModel = new CameraAddPopUpViewModel();

            var cameraAddPopUpView = new CameraAddPopUpView();
            cameraAddPopUpView.DataContext = cameraAddPopUpViewModel;

            return cameraAddPopUpView;
        }
    }
}
