using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace Optimizely.Labs.WelcomeDAM
{
    [Component]
    public class WelcomeNavigationComponent : ComponentDefinitionBase
    {
        public WelcomeNavigationComponent()
            : base("epi-cms/component/Media")
        {
            Categories = new string[] { "content" };
            LanguagePath = "/welcome/components/welcomedam";
            SortOrder = 500;
            this.Title = WelcomeConstants.ProviderName;
            PlugInAreas = new string[] { PlugInArea.AssetsDefaultGroup };
            Settings.Add(new Setting("repositoryKey", WelcomeConstants.ProviderKey));
        }
    }
}