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
    }
}