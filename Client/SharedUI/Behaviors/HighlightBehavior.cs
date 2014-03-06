using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SharedUI.Behaviors
{
    public class HighlightBehavior
    {
        public static readonly DependencyProperty HighlightOnFocusProperty =
            DependencyProperty.RegisterAttached("HighlightOnFocus", typeof(bool), typeof(HighlightBehavior),
            new PropertyMetadata(false, OnHighlightOnFocusPropertyChanged));
        
        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static bool GetHighlightOnFocus(UIElement element)
        {
            return (bool)element.GetValue(HighlightOnFocusProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static void SetHighlightOnFocus(UIElement element, bool value)
        {
            element.SetValue(HighlightOnFocusProperty, value);
        }
        
        private static void OnHighlightOnFocusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            var element = d as Control;
            if (element != null)
            {
                if (value)
                {
                    element.AddHandler(Control.GotFocusEvent, new RoutedEventHandler(OnGotFocus));
                    element.AddHandler(Control.LostFocusEvent, new RoutedEventHandler(OnLostFocus));
                    element.Tag = element.Background;
                }
                else
                {
                    element.RemoveHandler(Control.GotFocusEvent, new RoutedEventHandler(OnGotFocus));
                    element.RemoveHandler(Control.LostFocusEvent, new RoutedEventHandler(OnLostFocus));
                    element.Tag = null;
                }
            }
        }

        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            var element = sender as Control;
            if (element.IsEnabled && (element is TextBoxBase && !((TextBoxBase)element).IsReadOnly))
                element.Background = new SolidColorBrush(Color.FromRgb(232, 237, 247));
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var element = sender as Control;
            element.Background = element.Tag as Brush;
        }
    }
}
