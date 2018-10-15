using SafePlace.Models;
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
        private IFloorService _floorService;

        private IList<Floor> _floors;

        public HomePagePresenter(HomePageViewModel viewModel, ILogger logger, SynchronizationContext synchronizationContext, IFloorService floorService)
        {
            _viewModel = viewModel;
            _logger = logger;
            _synchronizationContext = synchronizationContext;
            _floorService = floorService;

            LoadFloorList();
            BuildViewModel();
        }

        private void BuildViewModel()
        {
            _viewModel.CurrentFloorImage = _floors[0].FloorMap;
            foreach (Floor floor in _floors)
            {
                _viewModel.FloorList.Add(floor.FloorName);
            }

            _viewModel.CameraClickCommand = new RelayCommand(o => Console.WriteLine("Camera click detected."));
        }

        private void LoadFloorList()
        {
            _floors = _floorService.GetFloorList().ToList();
        }


    }
}
