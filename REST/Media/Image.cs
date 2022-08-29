using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class Image : BaseRestFile
    {
        [JsonProperty("attribution_text")]
        public string AttributionText { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; } = new Source();

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}