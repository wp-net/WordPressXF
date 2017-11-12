using System;
using System.Globalization;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    public class SelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectedItemChangedEventArgs eventArgs))
                return null;

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
