using Services.SessionServiceReference;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace SessionModule.Assets.Converters
{
    public class TracksToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var currentTrack = parameter as Track;
            var trackList = value as ObservableCollection<Track>;
            if (trackList != null && currentTrack != null)
            {
                foreach (var track in trackList)
                {
                    if (currentTrack.Id == track.Id)
                        return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
