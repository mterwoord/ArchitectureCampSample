using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SharedUI.Converters
{
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var imageByte = (Byte[])value;
            var stream = new MemoryStream(imageByte);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();
            stream.Dispose();
            stream = null;
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var imageSource = value as BitmapImage;
            if (imageSource == null) return null;
            var bytes = new byte[] { };
            var bi = imageSource;
            using (MemoryStream stream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));
                encoder.Save(stream);
                bytes = stream.ToArray();
                stream.Close();
            }
            return bytes;
        }
    }
}
