using SafePlace.Service;
using SafePlaceFaceRecognition.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceFaceRecognition
{
    public class FaceRecognition
    {
        private static FaceRecognition _instance = null;
        private static readonly object _lock = new object();
        private ICamerasManager _camerasManager;
        private IFaceRecognitionService _faceRecognitionService;
        public static FaceRecognition GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new FaceRecognition();
                }
                return _instance;
            }
        }
        private FaceRecognition()
        {
            _faceRecognitionService = new FaceRecognitionService(
                ConfigurationManager.AppSettings["azure-keys"],
                ConfigurationManager.AppSettings["azure-endpoint"],
                ConfigurationManager.AppSettings["azure-group-id"],
                new ConsoleLogger()
                );
            _camerasManager = new CamerasManager(_faceRecognitionService);
        }

        public ICamerasManager GetCamerasManager()
        {
            return _camerasManager;
        }

        public IFaceRecognitionService GetFaceRecognitionService()
        {
            return _faceRecognitionService;
        }

    }
}
