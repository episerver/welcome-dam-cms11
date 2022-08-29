using EPiServer.Shell;
using WelcomeDAM.Models;

namespace WelcomeDAM.UIDescriptors
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