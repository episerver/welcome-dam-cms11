using EPiServer.Shell;
using Optimizely.Labs.WelcomeDAM.Models;

namespace Optimizely.Labs.WelcomeDAM.UIDescriptors
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