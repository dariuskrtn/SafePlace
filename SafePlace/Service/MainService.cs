using System;
using System.Collections.Generic;
using System.Configuration;
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
        private IFaceRecognitionService _faceRecognitionService;

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
        public IFaceRecognitionService GetFaceRecognitionServiceInstance()
        {
            lock (_lock)
            {
                if (_faceRecognitionService == null)
                {
                    _faceRecognitionService = new FaceRecognitionService(
                        ConfigurationManager.AppSettings["azure-key"],
                        ConfigurationManager.AppSettings["azure-endpoint"],
                        ConfigurationManager.AppSettings["azure-group-id"],
                        GetLoggerInstance());
                }
                return _faceRecognitionService;
            }
        }
        public SynchronizationContext GetSynchronizationContext()
        {
            return _synchronizationContext;
        }
    }
}
