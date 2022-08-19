using EPiServer.Shell;
using WelcomeDAM.Models;

namespace WelcomeDAM.UIDescriptors
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