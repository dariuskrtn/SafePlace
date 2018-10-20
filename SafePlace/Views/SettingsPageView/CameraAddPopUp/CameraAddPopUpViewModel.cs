using SafePlace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Views.CameraAddPopUp
{
    class CameraAddPopUpViewModel : BaseViewModel
    {
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
        private string _IPAdress;
        public string IPAdress
        {
            get
            {
                return _IPAdress;
            }
            set
            {
                _IPAdress = value;
                NotifyPropertyChanged();
            }
        }
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

    }
}
