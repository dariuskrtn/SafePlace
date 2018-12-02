using AForge.Video;
using SafePlace.DTO;
using SafePlace.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace SafePlaceFaceRecognition.Service
{
    class CameraAnalyzeService : ICameraAnalyzeService
    {
        public int RequestPeriod { get; set; } = 3000;

        private Subject<SpottedPeople> _subject = new Subject<SpottedPeople>();
        private readonly IFaceRecognitionService _recognitionService;
        private MJPEGStream _stream;
        private Camera _camera;
        private Bitmap _lastFrame;
        private bool _isStopped;
        private bool _isRunning;


        public CameraAnalyzeService(IFaceRecognitionService recognitionService, Camera camera)
        {
            _recognitionService = recognitionService;
            _camera = camera;
            _stream = new MJPEGStream(camera.IPAddress);
            _stream.NewFrame += OnFrameReceived;
            _stream.Start();
        }

        private void OnFrameReceived(object sender, NewFrameEventArgs eventArgs)
        {
            _lastFrame = (Bitmap)eventArgs.Frame.Clone();
        }

        public void Start()
        {
            _isStopped = false;
            if (!_isRunning)
            {
                new Thread(_ => ServiceThread()).Start();
            }
        }

        public void Stop()
        {
            _isStopped = true;
        }

        private async void ServiceThread()
        {
            _isRunning = true;
            while (!_isStopped)
            {
                if (_lastFrame == null)
                {
                    _camera.IdentifiedPeople.Clear();
                    _camera.Status = SafePlace.Enums.CameraStatus.Offline;
                } else
                {
                    var results = await _recognitionService.RecognizePeople(_lastFrame);
                    if (results == null) results = Enumerable.Empty<Guid>();
                    
                    _subject.OnNext(new SpottedPeople() { Camera =  _camera.Guid, spottedPeople = results.Select(item => new SpottedPerson(_camera.Guid, item)) });
                }
                
                Thread.Sleep(RequestPeriod);
            }
            _isRunning = false;
        }

        public IObservable<SpottedPeople> GetCameraUpdateObservable()
        {
            return _subject;
        }
    }
}
