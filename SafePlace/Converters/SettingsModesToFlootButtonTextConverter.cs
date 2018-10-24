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
    class SettingsModesToFlootButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SettingsModes)value)
            {
                case SettingsModes.Preview:
                    return "Create new Floor";
                case SettingsModes.Editing:
                    return "Save changes";
                case SettingsModes.CreatingNew:
                    return "Save new floor";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
