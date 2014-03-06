using System.Windows;
using System.Windows.Controls;

namespace SharedUI.Controls.Flyouts
{
    public class NavigationButton : Button
    {
        public NavigationButton()
        {
            this.DefaultStyleKey = typeof(NavigationButton);
        }

        #region Direction

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(NavigationButtonDirection), typeof(NavigationButton), new PropertyMetadata(NavigationButtonDirection.Left));
        public NavigationButtonDirection Direction { get { return (NavigationButtonDirection)GetValue(DirectionProperty); } set { SetValue(DirectionProperty, value); } }

        #endregion
        
    }

    public enum NavigationButtonDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
