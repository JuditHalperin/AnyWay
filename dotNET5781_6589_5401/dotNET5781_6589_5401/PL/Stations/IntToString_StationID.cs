using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    class IntToString_StationID : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;
            if (id > 0)
                return id;
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
