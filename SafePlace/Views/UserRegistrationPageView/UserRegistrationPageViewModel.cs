using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebEye.Controls.Wpf;

namespace SafePlace.Views.UserRegistrationPageView
{
    class UserRegistrationPageViewModel : BaseViewModel
    {
        private bool _isCapturing;
        public bool IsCapturing
        {
            get
            {
                return _isCapturing;
            }
            set
            {
                _isCapturing = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isRecording;
        public bool IsRecording
        {
            get
            {
                return _isRecording;
            }
            set
            {
                _isRecording = value;
                NotifyPropertyChanged();
            }
        }
        private WebCameraId _webCameraId;
        public WebCameraId WebCameraId
        {
            get
            {
                return _webCameraId;
            }
            set
            {
                _webCameraId = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<WebCameraId> Webcams { get; set; } = new ObservableCollection<WebCameraId>();
        public ObservableCollection<Bitmap> Recordings { get; set; } = new ObservableCollection<Bitmap>();


        private ICommand _toggleCapturingCommand;
        public ICommand ToggleCapturingCommand
        {
            get
            {
                return _toggleCapturingCommand;
            }
            set
            {
                _toggleCapturingCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _recordImageCommand;
        public ICommand RecordImageCommand
        {
            get
            {
                return _recordImageCommand;
            }
            set
            {
                _recordImageCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _savePersonCommand;
        public ICommand SavePersonCommand
        {
            get
            {
                return _savePersonCommand;
            }
            set
            {
                _savePersonCommand = value;
                NotifyPropertyChanged();
            }
        }

        private int _requiredImagesCount;
        public int RequiredImagesCount
        {
            get
            {
                return _requiredImagesCount;
            }
            set
            {
                _requiredImagesCount = value;
                NotifyPropertyChanged();
            }
        }

        private int _currentImagesCount;
        public int CurrentImagesCount
        {
            get
            {
                return _currentImagesCount;
            }
            set
            {
                _currentImagesCount = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                NotifyPropertyChanged();
            }
        }

    }
}
