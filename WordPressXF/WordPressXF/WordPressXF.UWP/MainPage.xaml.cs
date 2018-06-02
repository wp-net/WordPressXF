using Xamarin.Forms.Platform.UWP;

namespace WordPressXF.UWP
{
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new WordPressXF.App());
        }
    }
}
