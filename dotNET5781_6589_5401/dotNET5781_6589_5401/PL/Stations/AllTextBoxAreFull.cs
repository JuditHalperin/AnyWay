using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Stations
{
    /// <summary>
    /// enable 'Ok' button when all TextBox are filled with some text
    /// </summary>
    public class AllTextBoxAreFull : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (object val in values)
                if (string.IsNullOrEmpty(val as string))
                    return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
