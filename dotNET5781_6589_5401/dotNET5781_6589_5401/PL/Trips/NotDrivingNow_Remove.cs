using BLAPI;
using BO;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL
{
    /// <summary>
    /// Do not allow to remove a trip if it is happening right now
    /// </summary>
    class NotDrivingNow_Remove : IMultiValueConverter
    {
        static IBL bl = BlFactory.GetBl();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(DateTime.Now.TimeOfDay >= (TimeSpan)values[0] && DateTime.Now.TimeOfDay <= (TimeSpan)values[1] + bl.duration(bl.getLine((int)values[2]).Path).SecondsToTimeSpan())
                    return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}