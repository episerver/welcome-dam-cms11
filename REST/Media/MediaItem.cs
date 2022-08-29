using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class MediaItem : BaseRestItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("links")]
        public Link Links { get; set; }
    }
}