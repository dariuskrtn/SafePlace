using AForge.Video;
using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.CameraWindowView
{
    class CameraWindowViewModel : BaseViewModel
    {
        private MJPEGStream _stream;
        public MJPEGStream Stream
        {
            get
            {
                return _stream;
            }
            set
            {
                _stream = value;
                NotifyPropertyChanged();
            }
        }
    }
}
