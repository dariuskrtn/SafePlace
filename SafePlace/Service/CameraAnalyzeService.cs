using AForge.Video;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class CameraAnalyzeService : ICameraAnalyzeService
    {
        public int RequestPeriod { get; set; }

        private readonly IFaceRecognitionService _recognitionService;
        private MJPEGStream _stream;
        private Bitmap _lastFrame;
        private bool _isStopped;
        private bool _isRunning;


        public CameraAnalyzeService(IFaceRecognitionService recognitionService, Camera camera)
        {
            _recognitionService = recognitionService;
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
                var results = await _recognitionService.RecognizePeople(_lastFrame);
                if (results != null)
                foreach (var res in results)
                {
                    //Console.WriteLine(res.ToString());
                }
                Thread.Sleep(RequestPeriod);
            }
            _isRunning = false;
        }
    }
}
