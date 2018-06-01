using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views.AppShell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShellPage : MasterDetailPage
    {
        public AppShellPage()
        {
            InitializeComponent();

            MasterPage.PrimaryListView.ItemSelected += ListViewOnItemSelected;
            MasterPage.SecondaryListView.ItemSelected += ListViewOnItemSelected;
        }

        private void ListViewOnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is AppShellMenuItem item))
                return;

            ((ListView)sender).SelectedItem = null;

            if (item.TargetType == null)
                return;

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                if (((NavigationPage)Detail).InternalChildren != null && ((NavigationPage)Detail).InternalChildren.Count > 0 && ((NavigationPage)Detail).InternalChildren[0].GetType() == item.TargetType)
                {
                    IsPresented = false;
                    return;
                }
            }

            var page = (Page)Activator.CreateInstance(item.TargetType);
            var navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(navigationPage, false);

            Detail.Navigation.PushAsync(navigationPage);
            IsPresented = false;
        }
    }
}