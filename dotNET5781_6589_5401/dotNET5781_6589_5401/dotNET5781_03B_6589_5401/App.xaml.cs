using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace dotNET5781_03B_6589_5401
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

    }

    /// <summary>
    /// convert a status of type 'State' to string
    /// used to present the buses status on the main window and on the details window
    /// </summary>
    public class StatusToText_Status : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State stateValue = (State)value;

            switch (stateValue)
            {
                case State.canDrive:
                    return "Can Drive";
                case State.cannotDrive:
                    return "Cannot Drive";
                case State.gettingFueled:
                    return "Being fueled";
                case State.gettingTreated:
                    return "Being serviced";
                case State.driving:
                    return "Driving";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
