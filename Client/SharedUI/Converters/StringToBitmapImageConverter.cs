using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SharedUI.Converters
{
    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return Application.Current.TryFindResource(value.ToString() + "Image") as BitmapImage;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage)
            {
                return ((BitmapImage)value).UriSource.AbsolutePath;
            }
            return null;
        }
    }
}
