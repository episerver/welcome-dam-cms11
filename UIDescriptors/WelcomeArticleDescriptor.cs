using EPiServer.Shell;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.UIDescriptors
{
    [UIDescriptorRegistration]
    public class WelcomeArticleDescriptor : UIDescriptor<WelcomeArticleFile>
    {
        public WelcomeArticleDescriptor()
          : base("epi-iconEdited")
        {
            DefaultView = CmsViewNames.OnPageEditView;
            DisabledViews = System.Array.Empty<string>();
        }
    }
}