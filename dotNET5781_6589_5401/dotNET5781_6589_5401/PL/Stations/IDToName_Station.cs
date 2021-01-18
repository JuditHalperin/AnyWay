using BLAPI;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// Get the name of a line station by searching for the original station
    /// </summary>
    class IDToName_Station : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BlFactory.GetBl().getStation((int)value).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
