using System;
using System.Globalization;
using System.Windows.Data;

namespace SharedUI.Converters
{
    public class DoubleRatingToIntRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            var doubleValue = System.Convert.ToDouble(value.ToString());
            double doubleValue2 = (doubleValue) / 5;
            return doubleValue2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            var doubleValue = System.Convert.ToDouble(value.ToString());
            double double2 = (doubleValue) * 5;
            return double2;
        }
    }
}
