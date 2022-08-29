using EPiServer.Shell;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.UIDescriptors
{
    [UIDescriptorRegistration]
    public class WelcomeRawFileDescriptor : UIDescriptor<WelcomeRawFile>
    {
        public WelcomeRawFileDescriptor()
          : base(ContentTypeCssClassNames.Unknown)
        {
            DefaultView = CmsViewNames.OnPageEditView;
            DisabledViews = System.Array.Empty<string>();
        }
    }
}