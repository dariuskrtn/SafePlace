using SafePlace.Service;
using SafePlaceWpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlaceWpf.Service
{
    class DependencyInjector
    {
        private readonly SynchronizationContext _synchronizationContext;
        private readonly object _lock = new object();

        private IPageCreator _pageCreator;
        private IWindowCreator _windowCreator;
        public DependencyInjector()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }
        public SynchronizationContext GetSynchronizationContext()
        {
            return _synchronizationContext;
        }
        public IMainService GetMainService()
        {
            return MainService.GetInstance();
        }

        public IPageCreator GetPageCreator()
        {
            lock (_lock)
            {
                if (_pageCreator == null)
                {
                    _pageCreator = new PageCreator(GetMainService(), _synchronizationContext);
                }
                return _pageCreator;
            }
        }

        public IWindowCreator GetWindowCreator()
        {
            lock (_lock)
            {
                if (_windowCreator == null)
                {
                    _windowCreator = new WindowCreator(GetMainService());
                }
                return _windowCreator;
            }
        }

    }
}
