using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class ImageResolution
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}