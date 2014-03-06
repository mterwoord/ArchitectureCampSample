using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;

namespace SharedUI.Controls.Flyouts
{
    public class NavigationView : Control
    {
        private Grid _grid;
        private Button _backButton;

        public NavigationView()
        {
            this.DefaultStyleKey = typeof(NavigationView);
            this.PreviewKeyDown += this.OnPreviewKeyDown;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.Left && Keyboard.Modifiers == ModifierKeys.Alt &&
                this.ActiveRegion == NavigationRegionType.Detail)
                this.ActiveRegion = NavigationRegionType.Master;
        }

        public override void OnApplyTemplate()
        {
            _grid = this.GetTemplateChild("LayoutRoot") as Grid;
            _backButton = this.GetTemplateChild("PART_BackButton") as Button;
            if (_backButton != null)
                _backButton.Click += this.OnBackButtonClick;
            this.ActiveHeaderText = this.ActiveRegion == NavigationRegionType.Master ? this.MasterHeaderText : this.DetailHeaderText;
        }

        #region MasterView

        public static readonly DependencyProperty MasterViewProperty =
            DependencyProperty.Register("MasterView", typeof(FrameworkElement), typeof(NavigationView), new PropertyMetadata(null));
        public FrameworkElement MasterView { get { return (FrameworkElement)GetValue(MasterViewProperty); } set { SetValue(MasterViewProperty, value); } }

        #endregion

        #region DetailView

        public static readonly DependencyProperty DetailViewProperty =
            DependencyProperty.Register("DetailView", typeof(FrameworkElement), typeof(NavigationView), new PropertyMetadata(null));
        public FrameworkElement DetailView { get { return (FrameworkElement)GetValue(DetailViewProperty); } set { SetValue(DetailViewProperty, value); } }

        #endregion

        #region ActiveRegion

        public static readonly DependencyProperty ActiveRegionProperty =
            DependencyProperty.Register("ActiveRegion", typeof(NavigationRegionType), typeof(NavigationView), new UIPropertyMetadata(NavigationRegionType.Master, OnActiveRegionPropertyChanged));
        public NavigationRegionType ActiveRegion { get { return (NavigationRegionType)GetValue(ActiveRegionProperty); } set { SetValue(ActiveRegionProperty, value); } }
        private static void OnActiveRegionPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((NavigationView)sender).OnActiveRegionChanged(sender, e);
        }
        
        private void OnActiveRegionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var regionType = (NavigationRegionType)e.NewValue;
            if (_grid != null)
            {
                var state = regionType == NavigationRegionType.Master ? "Normal" : "Detail";
                VisualStateManager.GoToElementState(_grid, state, false);
            }
            this.ActiveHeaderText = regionType == NavigationRegionType.Master ?
                this.MasterHeaderText : this.DetailHeaderText;
            this.CanGoBack = regionType == NavigationRegionType.Detail;
        }

        #endregion

        #region CanGoBack

        public static readonly DependencyProperty CanGoBackProperty =
            DependencyProperty.Register("CanGoBack", typeof(bool), typeof(NavigationView), new PropertyMetadata(false));
        public bool CanGoBack { get { return (bool)GetValue(CanGoBackProperty); } set { SetValue(CanGoBackProperty, value); } }

        #endregion

        #region MasterHeader

        public static readonly DependencyProperty MasterHeaderTextProperty =
            DependencyProperty.Register("MasterHeaderText", typeof(string), typeof(NavigationView), new PropertyMetadata(""));
        public string MasterHeaderText { get { return (string)GetValue(MasterHeaderTextProperty); } set { SetValue(MasterHeaderTextProperty, value); } }

        #endregion

        #region DetailHeader

        public static readonly DependencyProperty DetailHeaderTextProperty =
            DependencyProperty.Register("DetailHeaderText", typeof(string), typeof(NavigationView), new PropertyMetadata(""));
        public string DetailHeaderText { get { return (string)GetValue(DetailHeaderTextProperty); } set { SetValue(DetailHeaderTextProperty, value); } }

        #endregion

        #region ActiveHeaderText

        public static readonly DependencyProperty ActiveHeaderTextProperty =
            DependencyProperty.Register("ActiveHeaderText", typeof(string), typeof(NavigationView), new PropertyMetadata(""));
        public string ActiveHeaderText { get { return (string)GetValue(ActiveHeaderTextProperty); } set { SetValue(ActiveHeaderTextProperty, value); } }

        #endregion

        #region HeaderCommandArea

        public static readonly DependencyProperty HeaderCommandAreaProperty =
            DependencyProperty.Register("HeaderCommandArea", typeof(object), typeof(NavigationView), new PropertyMetadata());
        public object HeaderCommandArea { get { return (object)GetValue(HeaderCommandAreaProperty); } set { SetValue(HeaderCommandAreaProperty, value); } }

        #endregion

        public event EventHandler NavigatedBack;

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            this.ActiveRegion = NavigationRegionType.Master;
            if (this.NavigatedBack != null)
                this.NavigatedBack(this, EventArgs.Empty);
        }

    }

    public enum NavigationRegionType
    {
        Master,
        Detail
    }
}
