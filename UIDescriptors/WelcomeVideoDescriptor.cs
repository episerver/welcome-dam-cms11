using EPiServer.Shell;

namespace WelcomeDAM.UIDescriptors
{
    [UIDescriptorRegistration]
    public class WelcomeVideoDescriptor : UIDescriptor<WelcomeVideoDescriptor>
    {
        public WelcomeVideoDescriptor()
          : base(ContentTypeCssClassNames.Video)
        {
            DefaultView = CmsViewNames.OnPageEditView;
            DisabledViews = System.Array.Empty<string>();
        }
    }
}