using SafePlace.Views.HomePageView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SafePlace.Service
{
    class PageCreator : IPageCreator
    {
        private readonly IMainService _mainService;

        public PageCreator(IMainService mainService)
        {
            _mainService = mainService;
        }

        public Page CreateHomePage()
        {
            var homePageViewModel = new HomePageViewModel();
            var homePagePresenter = new HomePagePresenter(homePageViewModel, _mainService.GetLoggerInstance(), _mainService.GetSynchronizationContext());

            var homePageView = new HomePageView();
            homePageView.DataContext = homePageViewModel;

            return homePageView;
        }
    }
}
