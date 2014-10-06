using System;
using System.Windows.Data;

namespace PersonnalLibrary.Components.Converters
{
    public class NegativeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double result;
            try
            {
                result = (double)value;
                result = -result;
            }
            catch (InvalidCastException ex)
            {
                result = 0;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
