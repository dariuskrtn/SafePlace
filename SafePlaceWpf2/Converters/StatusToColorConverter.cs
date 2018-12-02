using SafePlace.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SafePlaceWpf.Converters
{
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Ellipse is filled using an object of type SolidColorBrush.

            switch ((CameraStatus)value )
            {
                case CameraStatus.Offline:
                    return Brushes.Black;
                case CameraStatus.Good:
                    return Brushes.Green;
                case CameraStatus.Error:
                    return Brushes.Red;
                case CameraStatus.Empty:
                    return Brushes.Blue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
