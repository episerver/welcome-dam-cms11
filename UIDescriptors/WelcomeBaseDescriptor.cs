using EPiServer.Shell;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.UIDescriptors
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