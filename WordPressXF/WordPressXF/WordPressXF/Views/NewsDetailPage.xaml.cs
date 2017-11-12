using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetailPage : ContentPage
    {
        public NewsDetailPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                Icon = new FileImageSource { File = "post.png" };
        }

        private void WebViewOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (!e.Url.StartsWith("http"))
                return;

            if (e.Url.StartsWith("https://syndication.twitter.com/") || e.Url.StartsWith("https://platform.twitter.com/"))
                return;

            e.Cancel = true;
            Device.OpenUri(new Uri(e.Url));
        }
    }
}