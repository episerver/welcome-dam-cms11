using System.Collections.Generic;
using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class MediaPageList
    {
        [JsonProperty("data")]
        public List<MediaItem> Assets { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}