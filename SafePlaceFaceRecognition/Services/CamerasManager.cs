using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using SafePlace.DTO;
using SafePlace.Models;

namespace SafePlaceFaceRecognition.Service
{
    class CamerasManager : ICamerasManager
    {
        private IObservable<SpottedPeople> _spottedPersonObservable = Observable.Empty<SpottedPeople>();
        private IDisposable _observableDisposable = null;
        private readonly ICollection<ICameraAnalyzeService> _analyzeServices = new Collection<ICameraAnalyzeService>();
        private readonly IFaceRecognitionService _recognitionService;
        private readonly Dictionary<Guid, SpottedPeople> _spottedPeople = new Dictionary<Guid, SpottedPeople>();

        public CamerasManager(IFaceRecognitionService recognitionService)
        {
            _recognitionService = recognitionService;
        }

        private ICameraAnalyzeService CreateAnalyzeService(Camera camera)
        {
            var analyzer = new CameraAnalyzeService(_recognitionService, camera);
            _analyzeServices.Add(analyzer);
            return analyzer;
        }
        public void AddCamera(Camera camera)
        {
            var analyzer = CreateAnalyzeService(camera);
            analyzer.Start();

            _spottedPersonObservable = _spottedPersonObservable.Merge(analyzer.GetCameraUpdateObservable());

            if (_observableDisposable != null) _observableDisposable.Dispose();

            _observableDisposable = _spottedPersonObservable.Subscribe(item =>
            {
                if (_spottedPeople.ContainsKey(item.Camera))
                {
                    _spottedPeople.Remove(item.Camera);
                }
                _spottedPeople.Add(item.Camera, item);
            });
            
        }

        public void AddCameras(IEnumerable<Camera> cameras)
        {
            foreach (var cam in cameras) {
                AddCamera(cam);
            }
        }

        public IEnumerable<SpottedPerson> GetSpottedPeople()
        {
            return _spottedPeople.Select(item => item.Value.spottedPeople).SelectMany(x => x);
        }
    }
}
