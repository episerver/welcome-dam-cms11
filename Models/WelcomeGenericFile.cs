using System.ComponentModel.DataAnnotations;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace Optimizely.Labs.WelcomeDAM.Models
{
    [ContentType(DisplayName = "Welcome Generic File", GroupName = "Welcome", GUID = "B878B928-DB17-4CFC-86E8-033C3F9B2A4C", AvailableInEditMode = false)]
    public class WelcomeGenericFile : WelcomeAssetData
    {
        [UIHint(UIHint.Textarea)]
        public virtual string Description { get; set; }

        public virtual double FileSize { get; set; }

        public virtual string MimeType { get; set; }
    }
}