using CommonServiceLocator;
using WordPressXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : TabbedPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<SettingsViewModel>();
        }
    }
}