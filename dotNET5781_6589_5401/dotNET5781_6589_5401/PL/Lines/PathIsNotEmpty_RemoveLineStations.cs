using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// Cannot remove a station from a line path when it is empty
    /// (used in both AddLine and EditLine)
    /// </summary>
    class PathIsNotEmpty_RemoveLineStations : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}