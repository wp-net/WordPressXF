using WordPressXF.Init;
using WordPressXF.Views;
using Xamarin.Forms;

namespace WordPressXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Bootstrapper.RegisterDependencies();

            MainPage = new SplashScreen();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
