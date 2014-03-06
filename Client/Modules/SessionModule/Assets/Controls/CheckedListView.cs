using Contracts;
using System.Windows;
using System.Windows.Controls;

namespace SessionModule.Assets.Controls
{
    public class CheckedListView : ListView
    {
        public CheckedListView()
        {
            this.DefaultStyleKey = typeof(CheckedListView);
        }

        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(object), typeof(CheckedListView), new PropertyMetadata(null));
    }

    public class CheckedDataItem : ModelBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; this.OnPropertyChanged(); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; this.OnPropertyChanged(); }
        }
    }
}
