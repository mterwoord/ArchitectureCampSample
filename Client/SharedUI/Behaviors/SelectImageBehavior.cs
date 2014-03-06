using Microsoft.Win32;
using SharedUI.Converters;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SharedUI.Behaviors
{
    public class SelectImageBehavior
    {
        public static readonly DependencyProperty SelectImageOnButtonClickProperty =
            DependencyProperty.RegisterAttached("SelectImageOnButtonClick", typeof(bool), typeof(SelectImageBehavior), new PropertyMetadata(false, OnSelectImageOnButtonClickPropertyChanged));
        private static void OnSelectImageOnButtonClickPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                var value = (bool)e.NewValue;
                if (value)
                    button.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnClick));
                else
                    button.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(OnClick));
            }
        }
        public static void OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Bild auswählen...";
            dialog.Filter = "Alle Dateien|*.*";
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var image = new BitmapImage();
                using (var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                var button = sender as Button;
                if (button != null)
                {
                    var imageControl = button.Content as Image;
                    if (imageControl != null)
                    {
                        var expression = imageControl.GetBindingExpression(Image.SourceProperty);
                        if (expression != null)
                        {
                            var source = expression.ResolvedSource;
                            if (source != null)
                            {
                                var property = source.GetType().GetProperty(expression.ResolvedSourcePropertyName);
                                if (property != null)
                                {
                                    var bytes = new BitmapImageToByteArrayConverter().Convert(image, typeof(BitmapImage), null, System.Threading.Thread.CurrentThread.CurrentCulture);
                                    property.SetValue(source, bytes);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static bool GetSelectImageOnButtonClick(UIElement element)
        {
            return (bool)element.GetValue(SelectImageOnButtonClickProperty);
        }
        public static void SetSelectImageOnButtonClick(UIElement element, bool value)
        {
            element.SetValue(SelectImageOnButtonClickProperty, value);
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(BitmapImage), typeof(SelectImageBehavior), new PropertyMetadata(null));
        public static BitmapImage GetImage(UIElement element)
        {
            return (BitmapImage)element.GetValue(ImageProperty);
        }
        public static void SetImage(UIElement element, BitmapImage value)
        {
            element.SetValue(ImageProperty, value);
        }
    }
}
