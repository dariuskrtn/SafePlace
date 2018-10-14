using SafePlace.Service;
using SafePlace.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Views.MainWindowView
{
    class MainWindowPresenter
    {
        private MainWindowViewModel _viewModel;
        private SynchronizationContext _synchronizationContext;
        private ILogger _logger;
        

        public MainWindowPresenter(MainWindowViewModel viewModel, ILogger logger, SynchronizationContext synchronizationContext)
        {
            _viewModel = viewModel;
            _logger = logger;
            _synchronizationContext = synchronizationContext;

            BuildViewModel();
        }

        private void BuildViewModel()
        {
            _viewModel.DisplayPage = null;
            _viewModel.HomePageCommand = new RelayCommand(e => OpenHomePageCommand());
        }

        private void OpenHomePageCommand()
        {
            Console.WriteLine("Hello");
        }
    }
}
