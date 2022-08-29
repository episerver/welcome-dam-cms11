using EPiServer.Shell;
using WelcomeDAM.Models;

namespace WelcomeDAM.UIDescriptors
{
    [UIDescriptorRegistration]
    public class WelcomeBaseDescriptor : UIDescriptor<WelcomeAssetData>
    {
        public WelcomeBaseDescriptor()
          : base(ContentTypeCssClassNames.Unknown)
        {
            DefaultView = CmsViewNames.OnPageEditView;
            DisabledViews = System.Array.Empty<string>();
        }
    }
}