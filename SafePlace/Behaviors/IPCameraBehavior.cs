using AForge.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SafePlace.Behaviors
{
    class IPCameraBehavior : Behavior<System.Windows.Controls.Image>
    {
        public static readonly DependencyProperty MJPEGStreamProperty = DependencyProperty.Register(
            "Stream", typeof(MJPEGStream), typeof(IPCameraBehavior), new PropertyMetadata(null, OnStreamAttached));

        public MJPEGStream Stream
        {
            get { return (MJPEGStream)GetValue(MJPEGStreamProperty); }
            set { SetValue(MJPEGStreamProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        private static void OnStreamAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as IPCameraBehavior;
            if (behavior.Stream != null)
            {
                behavior.Stream.Start();
                behavior.Stream.NewFrame += behavior.OnNewFrame;
            }
        }

        private void OnNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Dispatcher.Invoke(() =>
            {
                AssociatedObject.Source = Convert(eventArgs.Frame);
            });
        }

        public BitmapSource Convert(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

    }
}
