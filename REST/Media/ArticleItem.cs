using Newtonsoft.Json;
using System.Collections.Generic;

namespace WelcomeDAM.REST.Media
{
    public class ArticleItem : BaseRestItem, IPublicWelcomeUrl
    {
        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("meta_title")]
        public string MetaTitle { get; set; }

        [JsonProperty("meta_description")]
        public string MetaDescription { get; set; }

        [JsonProperty("meta_url")]
        public string MetaUrl { get; set; }

        [JsonProperty("meta_keywords")]
        public List<string> MetaKeywords { get; set; } = new List<string>();

        [JsonProperty("source_article")]
        public string SourceArticle { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("authors")]
        public List<Author> Authors { get; set; } = new List<Author>();

        [JsonProperty("lang_code")]
        public string LangCode { get; set; }

        [JsonProperty("pixel_key")]
        public string PixelKey { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; } = new List<Image>();
    }
}