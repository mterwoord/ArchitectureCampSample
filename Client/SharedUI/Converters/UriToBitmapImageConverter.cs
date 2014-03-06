using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SharedUI.Converters
{
    public class UriToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri uri = value as Uri;
            if (uri != null)
            {
                return new BitmapImage(uri);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage)
            {
                return ((BitmapImage)value).UriSource;
            }
            return null;
        }
    }
}
