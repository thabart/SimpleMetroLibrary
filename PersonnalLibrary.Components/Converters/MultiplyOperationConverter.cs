using System;
using System.Windows.Data;

namespace PersonnalLibrary.Components.Converters
{
    public class MultiplyOperationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = 0;
            if (values.Length >= 2 && values[0] != null && values[1] != null)
            {
                int firstValue,
                    secondValue;
                if (int.TryParse(values[0].ToString(), out firstValue) &&
                    int.TryParse(values[1].ToString(), out secondValue))
                {
                    result = firstValue * secondValue;
                }
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
