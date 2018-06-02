using Xamarin.Essentials;
using Xamarin.Forms;

namespace WordPressXF.Controls
{
    public class ExternalWebView : WebView
    {
        public ExternalWebView()
        {
            Navigating += ExternalWebViewOnNavigating;
        }

        private async void ExternalWebViewOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (!e.Url.StartsWith("http"))
                return;

            if (!e.Url.StartsWith("https"))
                return;

            if (e.Url.StartsWith("https://www.youtube.com/"))
                return;

            if (e.Url.StartsWith("https://syndication.twitter.com/") || e.Url.StartsWith("https://platform.twitter.com/"))
                return;

            e.Cancel = true;

            await Browser.OpenAsync(e.Url, BrowserLaunchType.External);
        }
    }
}
