using SafePlace.Enums;
using SafePlace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlaceWpf.ViewModels
{
    class CameraViewModel : BaseViewModel
    {
        private int _positionX;
        public int PositionX
         {
            get
            {
                return _positionX;
            }
            set
            {
                _positionX = value;
                NotifyPropertyChanged();
            }
        }
        private int _positionY;
        public int PositionY
        {
            get
            {
                return _positionY;
            }
            set
            {
                _positionY = value;
                NotifyPropertyChanged();
            }
        }
        private CameraStatus _status;
        public CameraStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value == _status)
                    return;
                _status = value;
                NotifyPropertyChanged();
            }
        }
        private Guid _guid;
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                if (value == _guid)
                    return;
                _guid = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Person> IdentifiedPeople { get; set; } = new ObservableCollection<Person>();

    }
}
