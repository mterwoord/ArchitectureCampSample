using SharedUI.Images;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SharedUI.Controls
{
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    public class AppButton : Button
    {

        #region Private Fields

        private Image _image;

        #endregion

        #region Constructors

        public AppButton()
        {
            this.DefaultStyleKey = typeof(AppButton);
        }

        #endregion

        #region Properties

        public AppButtonType ButtonType
        {
            get { return (AppButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(AppButtonType), typeof(AppButton),
            new PropertyMetadata(AppButtonType.Default, OnButtonTypePropertyChanged));

        private static void OnButtonTypePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((AppButton)sender).ApplyButtonType();
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            _image = this.GetTemplateChild("PART_Image") as Image;
            base.OnApplyTemplate();
            this.ApplyButtonType();
        }

        #endregion

        #region Private Methods

        private void ApplyButtonType()
        {
            if (_image == null)
                return;
            switch (this.ButtonType)
            {
                case AppButtonType.Default:
                    break;
                case AppButtonType.Ok:
                    this.Content = "OK";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Ok));
                    break;
                case AppButtonType.Yes:
                    this.Content = "Ja";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Ok));
                    break;
                case AppButtonType.Cancel:
                    this.Content = "Abbrechen";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.No:
                    this.Content = "Nein";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.Close:
                    this.Content = "Schließen";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Cancel));
                    break;
                case AppButtonType.Add:
                    this.Content = "Neu";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Add));
                    break;
                case AppButtonType.Delete:
                    this.Content = "Löschen";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Delete));
                    break;
                case AppButtonType.Edit:
                    this.Content = "Bearbeiten";
                    _image.SetValue(Image.SourceProperty, this.GetImage(ImageType.Edit));
                    break;
            }
            if (this.ButtonType != AppButtonType.Default)
                _image.Visibility = System.Windows.Visibility.Visible;
        }

        private BitmapImage GetImage(ImageType image)
        {
            return Application.Current.TryFindResource(image.ToString() + "Image") as BitmapImage;
        }

        #endregion

    }

    #region AppButtonType

    public enum AppButtonType
    {
        Default,
        Ok,
        Yes,
        Cancel,
        No,
        Close,
        Add,
        Delete,
        Edit
    }

    #endregion
}
