using System;
using System.Globalization;
using WordPressPCL.Models;
using WordPressXF.Utils;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    public class HtmlWebviewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Post post)
                return new HtmlWebViewSource { Html = HtmlTools.WrapContent(post) };

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
