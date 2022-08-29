using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Optimizely.Labs.WelcomeDAM.Models
{
    [ContentType(DisplayName = "Welcome Image", GUID = "EB66D7A5-BC54-4FA4-92A1-60336A7608D6", AvailableInEditMode = false, GroupName = "Welcome")]
    public class WelcomeImageFile : WelcomeGenericFile, IWelcomeThumbnail
    {
        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        [Ignore]
        public virtual string Thumbnail
        {
            get
            {
                var thumbnailUrl = UriUtil.AddQueryString(this.Url, "width", "150");
                return UriUtil.AddQueryString(thumbnailUrl, "height", "80");
            }
        }
    }
}