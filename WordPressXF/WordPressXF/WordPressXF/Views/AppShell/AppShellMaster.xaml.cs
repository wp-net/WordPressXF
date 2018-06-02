using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views.AppShell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShellMaster : ContentPage
    {
        public ListView PrimaryListView { get; set; }
        public ListView SecondaryListView { get; set; }

        public AppShellMaster()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                Icon = "hamburger.png";

            BindingContext = new AppShellMasterViewModel();

            PrimaryListView = Primary;
            SecondaryListView = Secondary;
        }
    }
}