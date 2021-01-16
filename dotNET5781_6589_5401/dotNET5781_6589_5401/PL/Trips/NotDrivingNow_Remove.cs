using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// do not allow to remove a trip if it is happrning now
    /// </summary>
    class NotDrivingNow_Remove : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(DateTime.Now.TimeOfDay >= (TimeSpan)values[0] && DateTime.Now.TimeOfDay <= (TimeSpan)values[1])
                    return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}