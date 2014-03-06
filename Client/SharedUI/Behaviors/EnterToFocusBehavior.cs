using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharedUI.Behaviors
{
    public class EnterToFocusBehavior
    {
        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.RegisterAttached("Button", typeof(Button), typeof(EnterToFocusBehavior),
            new PropertyMetadata(null, OnButtonPropertyChanged));

        private static void OnButtonPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Control ctl = sender as TextBox;
            if (ctl == null)
                ctl = sender as ComboBox;
            if (ctl == null) return;

            if (e.NewValue != null)
            {
                ctl.KeyDown += OnKeyDown;
            }
            else if (e.OldValue != null)
            {
                ctl.KeyDown -= OnKeyDown;
            }
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Control ctl = sender as TextBox;
                if (ctl != null)
                {
                    var exp = ctl.GetBindingExpression(TextBox.TextProperty);
                    if (exp != null)
                        exp.UpdateSource();
                }
                else if (sender is ComboBox)
                {
                    ctl = sender as ComboBox;
                    var exp = ctl.GetBindingExpression(ComboBox.TextProperty);
                    if (exp != null)
                        exp.UpdateSource();
                }
                else
                    return;
                
                var button = GetButton((UIElement)sender);
                if (button != null)
                    SimulateButtonClick(button);
            }
        }

        private static void SimulateButtonClick(Button button)
        {
            if (button == null) return;
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        public static void SetButton(UIElement element, Button button)
        {
            element.SetValue(ButtonProperty, button);
        }

        [AttachedPropertyBrowsableForType(typeof(Control))]
        public static Button GetButton(UIElement element)
        {
            return (Button)element.GetValue(ButtonProperty);
        }
    }
}
