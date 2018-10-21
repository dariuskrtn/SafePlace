using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using WebEye.Controls.Wpf;

namespace SafePlace.Behaviors
{
    class WebCameraBehavior : Behavior<WebCameraControl>
    {

        public static readonly DependencyProperty CapturingProperty = DependencyProperty.Register(
            "Capturing", typeof(bool), typeof(WebCameraBehavior), new PropertyMetadata(false, OnCapturingChanged));

        public static readonly DependencyProperty RecordingProperty = DependencyProperty.Register(
            "Recording", typeof(bool), typeof(WebCameraBehavior), new PropertyMetadata(false, OnRecordingChanged));

        public static readonly DependencyProperty WebCameraIdProperty = DependencyProperty.Register(
            "WebCameraId", typeof(WebCameraId), typeof(WebCameraBehavior), new PropertyMetadata(null, OnWebCameraIdChanged));

        public static readonly DependencyProperty WebCamerasCollectionProperty = DependencyProperty.Register(
            "WebCamerasCollection", typeof(ICollection<WebCameraId>), typeof(WebCameraBehavior), new PropertyMetadata(null, OnWebCamerasCollectionChanged));

        public static readonly DependencyProperty RecordingsCollectionProperty = DependencyProperty.Register(
            "RecordingsCollection", typeof(ICollection<Bitmap>), typeof(WebCameraBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty FramerateProperty = DependencyProperty.Register(
            "Framerate", typeof(int), typeof(WebCameraBehavior), new PropertyMetadata(1000));

        public bool Capturing
        {
            get { return (bool)GetValue(CapturingProperty); }
            set { SetValue(CapturingProperty, value); }
        }

        public bool Recording
        {
            get { return (bool)GetValue(RecordingProperty); }
            set { SetValue(RecordingProperty, value); }
        }

        public WebCameraId WebCameraId
        {
            get { return (WebCameraId)GetValue(WebCameraIdProperty); }
            set { SetValue(WebCameraIdProperty, value); }
        }

        public ICollection<WebCameraId> WebCamerasCollection
        {
            get { return (ICollection<WebCameraId>)GetValue(WebCamerasCollectionProperty); }
            set { SetValue(WebCamerasCollectionProperty, value); }
        }

        public ICollection<Bitmap> RecordingsCollection
        {
            get { return (ICollection<Bitmap>)GetValue(RecordingsCollectionProperty); }
            set { SetValue(RecordingsCollectionProperty, value); }
        }

        public int Framerate
        {
            get { return (int)GetValue(FramerateProperty); }
            set { SetValue(FramerateProperty, value); }
        }

        private bool _threadRunning = false;

        protected override void OnAttached()
        {
            base.OnAttached();

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        private static void OnWebCamerasCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as WebCameraBehavior;

            if (behavior.WebCamerasCollection != null)
            {
                behavior.WebCamerasCollection.Clear();
                foreach (WebCameraId webcam in behavior.AssociatedObject.GetVideoCaptureDevices())
                {
                    behavior.WebCamerasCollection.Add(webcam);
                }
            }
        }

        private static void OnCapturingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as WebCameraBehavior;

            if (behavior.Capturing) behavior.StartCapturing();
            else behavior.StopCapturing();
        }
        private static void OnRecordingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as WebCameraBehavior;
            if (behavior.Capturing && behavior.Recording) behavior.SaveImage();
        }
        private static void OnWebCameraIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as WebCameraBehavior;
            if (behavior.WebCameraId != null)
            {
                if (behavior.Capturing) behavior.StartCapturing();
            }
            else
            {
                behavior.StopCapturing();
            }
        }


        public void StartCapturing()
        {
            if (WebCameraId != null && !AssociatedObject.IsCapturing)
            {
                AssociatedObject.StartCapture(WebCameraId);
            }
        }

        public void StopCapturing()
        {
            if (AssociatedObject.IsCapturing)
            {
                AssociatedObject.StopCapture();
            }
        }

        public void SaveImage()
        {
            if (!Recording) return;

            if (RecordingsCollection != null && AssociatedObject.IsCapturing)
            {
                RecordingsCollection.Add(AssociatedObject.GetCurrentImage());
            }
            Recording = false;
        }

    }
}
