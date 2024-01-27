// Class that should be working to change color of buttons to show connectivity
// todo figure out why this stopped working 

using System;
using System.Globalization;
namespace ClientObserver.Models
{


    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Colors.Green : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
