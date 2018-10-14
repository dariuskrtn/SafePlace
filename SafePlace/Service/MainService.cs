using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class MainService : IMainService
    {
        private ILogger _logger;
        private IPageCreator _pageCreator;
        private SynchronizationContext _synchronizationContext;

        //Simple lock object to avoid multiple threads creating different class instances.
        private static readonly object _lock = new object();

        public MainService()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public ILogger GetLoggerInstance()
        {
            lock(_lock)
            {
                if (_logger == null)
                {
                    _logger = new ConsoleLogger();
                }
                return _logger;
            }
        }
        public IPageCreator GetPageCreatorInstance()
        {
            lock (_lock)
            {
                if (_pageCreator == null)
                {
                    _pageCreator = new PageCreator(this);
                }
                return _pageCreator;
            }
        }
        public SynchronizationContext GetSynchronizationContext()
        {
            return _synchronizationContext;
        }
    }
}
