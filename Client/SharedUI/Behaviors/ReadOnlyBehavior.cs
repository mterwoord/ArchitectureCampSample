using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SharedUI.Behaviors
{
    public class ReadOnlyBehavior
    {
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(ReadOnlyBehavior),
            new PropertyMetadata(false, OnIsReadOnlyPropertyChanged));
        
        public static bool GetIsReadOnly(UIElement element)
        {
            return (bool)element.GetValue(IsReadOnlyProperty);
        }
        
        public static void SetIsReadOnly(UIElement element, bool value)
        {
            element.SetValue(IsReadOnlyProperty, value);
        }

        private static void OnIsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d) && d is FrameworkElement)
            {
                var element = d as FrameworkElement;
                var isReadOnly = (bool)e.NewValue;
                if (isReadOnly)
                {
                    if (element.IsLoaded)
                        SetReadOnly(element, true);
                    else
                        element.AddHandler(FrameworkElement.LoadedEvent, new RoutedEventHandler(ReadOnlyBehavior_Loaded));
                }
                else
                {
                    element.RemoveHandler(FrameworkElement.LoadedEvent, new RoutedEventHandler(ReadOnlyBehavior_Loaded));
                    SetReadOnly(element, false);
                }
            }
        }

        private static void ReadOnlyBehavior_Loaded(object sender, RoutedEventArgs e)
        {
            SetReadOnly(sender as DependencyObject, true);
        }

        private static void SetReadOnly(DependencyObject parent, bool isReadOnly)
        {
            if (parent == null) return;
            var list = LogicalTreeHelper.GetChildren(parent);
            foreach (var element in list)
            {
                if (element is TextBoxBase)
                {
                    ((TextBoxBase)element).IsReadOnly = isReadOnly;
                }
                else if (element is Selector)
                {
                    ((Selector)element).IsEnabled = !isReadOnly;
                }
                else if (element is ButtonBase)
                {
                    ((ButtonBase)element).IsEnabled = !isReadOnly;
                }
                SetReadOnly(element as DependencyObject, isReadOnly);
            }
        }
    }
}
