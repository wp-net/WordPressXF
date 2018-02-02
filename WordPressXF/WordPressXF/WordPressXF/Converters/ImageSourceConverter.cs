using System;
using System.Globalization;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagePath = value as string;
            if (string.IsNullOrEmpty(imagePath))
                return null;

            var resource = ImageSource.FromResource(imagePath);
            return resource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
