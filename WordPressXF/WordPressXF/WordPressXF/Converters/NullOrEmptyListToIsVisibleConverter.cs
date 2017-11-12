using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    public class NullOrEmptyListToIsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case null:
                    return parameter == null;
                case IList list:
                    return list.Count == 0;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
