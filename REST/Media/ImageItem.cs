using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class ImageItem : BaseRestFile
    {
        [JsonProperty("image_resolution")]
        public ImageResolution ImageResolution { get; set; } = new ImageResolution();
    }
}