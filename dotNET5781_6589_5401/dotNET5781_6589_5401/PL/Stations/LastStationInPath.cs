using BLAPI;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// get the last station in the line path
    /// </summary>
    class LastStationInPath : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BlFactory.GetBl().getLastStation((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
