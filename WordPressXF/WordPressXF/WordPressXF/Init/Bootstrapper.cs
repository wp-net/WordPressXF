using CommonServiceLocator;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using WordPressXF.Services;
using WordPressXF.ViewModels;

namespace WordPressXF.Init
{
    public static class Bootstrapper
    {
        public static void RegisterDependencies()
        {
            var container = new UnityContainer();

            // service
            container.RegisterType<WordpressService>(new ContainerControlledLifetimeManager());

            // viewmodel
            container.RegisterType<NewsViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<SettingsViewModel>(new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
