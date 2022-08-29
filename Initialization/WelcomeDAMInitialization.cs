using System.Collections.Specialized;
using EPiServer.Cms.Shell.UI.Rest.Models.Transforms;
using EPiServer.Configuration;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Optimizely.Labs.WelcomeDAM.REST;

namespace Optimizely.Labs.WelcomeDAM.Initialization
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class WelcomeDAMInitialization : IInitializableModule, IConfigurableModule
    {
        void IConfigurableModule.ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += (o, e) =>
            {
                context.Services
                    .AddTransient<IWelcomeClient, WelcomeClient>()
                    .AddSingleton<IModelTransform, ThumbnailWelcomeTransform>();
            };
        }

        public void Initialize(InitializationEngine context)
        {
            // Create provider root if not exists
            var welcomeRoot = WelcomeContentProvider.GetRoot();

            // Load provider
            var contentProviderManager = context.Locate.Advanced.GetInstance<IContentProviderManager>();
            var configValues = new NameValueCollection { { ContentProviderElement.EntryPointString, welcomeRoot.ToString() } };
            var provider = context.Locate.Advanced.GetInstance<WelcomeContentProvider>();
            provider.Initialize(WelcomeConstants.ProviderKey, configValues);
            contentProviderManager.ProviderMap.AddProvider(provider);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}