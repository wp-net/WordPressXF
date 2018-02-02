using CommonServiceLocator;
using WordPressXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsOverviewPage : ContentPage
    {
        public NewsOverviewPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<NewsViewModel>();
        }
    }
}