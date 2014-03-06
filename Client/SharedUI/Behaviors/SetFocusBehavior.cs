using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SharedUI.Behaviors
{
    public class SetFocusBehavior
    {
        public static readonly DependencyProperty SetFocusToProperty =
            DependencyProperty.RegisterAttached("SetFocusTo", typeof(Control), typeof(SetFocusBehavior),
            new PropertyMetadata(null, OnSetFocusToPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static Control GetSetFocusTo(UIElement element)
        {
            return (Control)element.GetValue(SetFocusToProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static void SetSetFocusTo(UIElement element, Control value)
        {
            element.SetValue(SetFocusToProperty, value);
        }
        
        private static void OnSetFocusToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (Control)e.NewValue;
            var element = d as Button;
            if (element != null && !DesignerProperties.GetIsInDesignMode(element))
            {
                if (value != null)
                {
                    element.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnClick));
                }
                else
                {
                    element.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(OnClick));
                }
            }
        }

        private static void OnClick(object sender, RoutedEventArgs e)
        {
            var element = sender as Button;
            if (element != null)
            {
                var target = GetSetFocusTo(element);
                if (target != null)
                {
                    if (!target.IsEnabled)
                        target.IsEnabled = true;
                    else if (target is TextBoxBase && ((TextBoxBase)target).IsReadOnly)
                        ((TextBoxBase)target).IsReadOnly = false;
                    Keyboard.Focus(target);
                }
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var element = sender as Control;
            element.Background = element.Tag as Brush;
        }
    }
}
