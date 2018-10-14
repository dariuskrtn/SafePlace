using SafePlace.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        }
    }
}
