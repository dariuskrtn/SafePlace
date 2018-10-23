using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    public class MainService : IMainService
    {
        private ILogger _logger;
        private IPageCreator _pageCreator;
        private SynchronizationContext _synchronizationContext;
        private ICameraService _cameraService;
        private IFloorService _floorService;
        private IPersonService _personService;
        private IFaceRecognitionService _faceRecognitionService;
        private IWindowCreator _windowCreator;


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
                    //Instantiates face recognition service with parameters taken from App.config
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

        public IFloorService GetFloorServiceInstance()
        {
            lock (_lock)
            {
                if (_floorService == null)
                {
                    _floorService = new FloorService();
                }
                return _floorService;
            }
        }

        public ICameraService GetCameraServiceInstance()
        {
            lock (_lock)
            {
                if (_cameraService == null)
                {
                    _cameraService = new CameraService();
                }
                return _cameraService;
            }
        }

        public IPersonService GetPersonServiceInstance()
        {
            lock (_lock)
            {
                if (_personService == null)
                {
                    _personService = new PersonService();
                }
                return _personService;
            }
        }

        public IWindowCreator GetWindowCreatorInstance()
        {
            lock (_lock)
            {
                if (_windowCreator == null)
                {
                    _windowCreator = new WindowCreator();
                }
                return _windowCreator;
            }
        }
    }
}
