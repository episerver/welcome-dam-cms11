using EPiServer.Shell;
using WelcomeDAM.Models;

namespace WelcomeDAM.UIDescriptors
{
    [UIDescriptorRegistration]
    public class WelcomeImageDescriptor : UIDescriptor<WelcomeImageFile>
    {
        public WelcomeImageDescriptor()
          : base(ContentTypeCssClassNames.Image)
        {
            DefaultView = CmsViewNames.OnPageEditView;
            DisabledViews = System.Array.Empty<string>();
        }
    }
}