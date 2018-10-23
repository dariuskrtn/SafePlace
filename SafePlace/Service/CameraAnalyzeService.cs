using AForge.Video;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class CameraAnalyzeService : ICameraAnalyzeService
    {
        public int RequestPeriod { get; set; }

        private Subject<Camera> _subject = new Subject<Camera>();
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
                    _camera.Status = Enums.CameraStatus.Offline;
                } else
                {
                    var results = await _recognitionService.RecognizePeople(_lastFrame);
                    if (results == null) results = Enumerable.Empty<Person>();

                    _camera.IdentifiedPeople.Clear();

                    foreach (var res in results)
                    {
                        _camera.IdentifiedPeople.Add(res);
                    }

                    if (_camera.IdentifiedPeople.Count() == 0)
                    {
                        _camera.Status = Enums.CameraStatus.Empty;
                    }
                    else if (!_camera.IdentifiedPeople.Any(person => person.AllowedCameras.Contains(_camera.Guid)))
                    {
                        _camera.Status = Enums.CameraStatus.Error;
                    }
                    else
                    {
                        _camera.Status = Enums.CameraStatus.Good;
                    }
                }
                _subject.OnNext(_camera);
                
                Thread.Sleep(RequestPeriod);
            }
            _isRunning = false;
        }

        public IObservable<Camera> GetCameraUpdateObservable()
        {
            return _subject;
        }
    }
}
