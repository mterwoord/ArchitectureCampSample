using System.Windows;

namespace SharedUI.Behaviors
{
    public class VisualStateSelector
    {
        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.RegisterAttached("VisualState", typeof(string), typeof(VisualStateSelector),
            new PropertyMetadata(null, OnVisualStatePropertyChanged));
        
        public static string GetVisualState(UIElement element)
        {
            return (string)element.GetValue(VisualStateProperty);
        }

        public static void SetVisualState(UIElement element, string value)
        {
            element.SetValue(VisualStateProperty, value);
        }
        
        private static void OnVisualStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            var state = e.NewValue as string;
            if (element != null && !string.IsNullOrWhiteSpace(state))
            {
                VisualStateManager.GoToElementState(element, state, false);
            }
        }
    }
}
