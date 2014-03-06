using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharedUI.Controls
{
    public class CommandHeader : ItemsControl
    {
        #region Constructors

        public CommandHeader()
        {
            this.DefaultStyleKey = typeof(CommandHeader);
        }

        #endregion

        #region HeaderImage

        public ImageSource HeaderImage
        {
            get { return (ImageSource)GetValue(HeaderImageProperty); }
            set { SetValue(HeaderImageProperty, value); }
        }

        public static readonly DependencyProperty HeaderImageProperty =
            DependencyProperty.Register("HeaderImage", typeof(ImageSource), typeof(CommandHeader),
            new PropertyMetadata(null));

        #endregion

        #region HeaderText

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(CommandHeader),
            new PropertyMetadata(""));

        #endregion

        #region HeaderFontFamily

        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty HeaderFontFamilyProperty =
            DependencyProperty.Register("HeaderFontFamily", typeof(FontFamily), typeof(CommandHeader), new PropertyMetadata(null));

        #endregion

        #region HeaderFontSize

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(CommandHeader), new PropertyMetadata(double.NaN));

        #endregion
    }
}
