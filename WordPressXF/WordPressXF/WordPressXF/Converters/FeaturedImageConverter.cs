using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WordPressPCL.Models;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    public class FeaturedImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Embedded embedded))
                return string.Empty;

            if (embedded.WpFeaturedmedia == null)
                return string.Empty;

            var mediaList = new List<MediaItem>(embedded.WpFeaturedmedia);
            return mediaList.Any() ? mediaList.First().SourceUrl : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
