using System.Globalization;
using EPiServer.Core;

namespace WelcomeDAM.Models
{
    public abstract class WelcomeAssetData : ContentBase, IContent, IContentData, ILocale
    {
        public virtual string AssetId { get; set; }

        public virtual string Url { get; set; }

        public virtual string AssetType { get; set; }

        public virtual string FileExtension { get; set; }

        public virtual string FileLocation { get; set; }

        public CultureInfo Language { get; set; }

        public virtual bool IsPublic { get; set; }
    }
}