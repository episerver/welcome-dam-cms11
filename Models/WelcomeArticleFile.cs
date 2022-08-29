using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace Optimizely.Labs.WelcomeDAM.Models
{
    [ContentType(DisplayName = "Welcome Article", GUID = "1B46E9CF-6772-4769-97BD-2331BDD01318", AvailableInEditMode = false, GroupName = "Welcome")]
    public class WelcomeArticleFile : WelcomeAssetData
    {
        public virtual XhtmlString HtmlBody { get; set; }

        public virtual string MetaTitle { get; set; }

        public virtual string MetaDescription { get; set; }

        public virtual Url MetaUrl { get; set; }

        public virtual IList<string> MetaKeywords { get; set; }

        public virtual string ArticleSource { get; set; }

        public virtual IList<string> Authors { get; set; }

        //public virtual string Thumbnail { get; set; }
    }
}