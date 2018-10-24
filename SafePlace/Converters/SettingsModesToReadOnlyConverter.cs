using SafePlace.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SafePlace.Converters
{
    class SettingsModesToReadOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SettingsModes)value)
            {
                case SettingsModes.Preview:
                    return true;
                case SettingsModes.Editing:
                    return false;
                case SettingsModes.CreatingNew:
                    return false;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}