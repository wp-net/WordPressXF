using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo _ci;
        private const string ResourceId = "WordPressXF.Resources.AppResources";

        private static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            _ci = new CultureInfo("en-US");
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            return ResMgr.Value.GetString(Text, _ci) ?? Text;
        }
    }
}
