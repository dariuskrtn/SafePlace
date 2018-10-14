using SafePlace.Service;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Views.HomePageView
{
    /*
     * Moved Home Page from MainWindow.
     * Please check if all dependencies are required and remove/add the ones which are needed.
     */
    class HomePagePresenter
    {
        private HomePageViewModel _viewModel;
        private SynchronizationContext _synchronizationContext;
        private ILogger _logger;

        public HomePagePresenter(HomePageViewModel viewModel, ILogger logger, SynchronizationContext synchronizationContext)
        {
            _viewModel = viewModel;
            _logger = logger;
            _synchronizationContext = synchronizationContext;
        }
    }
}
