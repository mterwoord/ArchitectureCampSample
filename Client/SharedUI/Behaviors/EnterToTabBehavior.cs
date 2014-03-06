using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharedUI.Behaviors
{
    public class EnterToTabBehavior
    {
        public static readonly DependencyProperty FocusedControlProperty =
            DependencyProperty.RegisterAttached("FocusedControl", typeof(Control), typeof(EnterToTabBehavior), new PropertyMetadata(null, OnFocusedControlPropertyChanged));

        private static void OnFocusedControlPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Control ctl = sender as TextBox;
            if (ctl == null)
                ctl = sender as ComboBox;
            if (ctl == null) return;

            if (e.NewValue != null)
            {
                ctl.AddHandler(Control.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown));
            }
            else if (e.OldValue != null)
            {
                ctl.RemoveHandler(Control.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown));
            }
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (GetFocusNext((UIElement)sender))
                {
                    TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                    if (keyboardFocus != null)
                    {
                        keyboardFocus.MoveFocus(tRequest);
                    }
                }
                else if (GetFocusedControl((UIElement)sender) != null)
                {
                    var ctl = GetFocusedControl((UIElement)sender);
                    if (ctl != null && ctl is IInputElement)
                    {
                        FocusManager.SetFocusedElement(ctl, (IInputElement)ctl);
                    }
                }
                e.Handled = true;
            }
        }

        public static void SetFocusedControl(UIElement element, Control control)
        {
            element.SetValue(FocusedControlProperty, control);
        }

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static Control GetFocusedControl(UIElement element)
        {
            return (Control)element.GetValue(FocusedControlProperty);
        }

        public static readonly DependencyProperty FocusNextProperty =
            DependencyProperty.RegisterAttached("FocusNext", typeof(bool), typeof(EnterToTabBehavior), new PropertyMetadata(false, OnFocusNextPropertyChanged));

        private static void OnFocusNextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Control ctl = sender as TextBox;
            if (ctl == null)
                ctl = sender as ComboBox;
            if (ctl == null) return;

            if (e.NewValue != null)
            {
                ctl.AddHandler(Control.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown));
            }
            else if (e.OldValue != null)
            {
                ctl.RemoveHandler(Control.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown));
            }
        }

        public static void SetFocusNext(UIElement element, bool focusNext)
        {
            element.SetValue(FocusNextProperty, focusNext);
        }

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static bool GetFocusNext(UIElement element)
        {
            return (bool)element.GetValue(FocusNextProperty);
        }
    }
}
