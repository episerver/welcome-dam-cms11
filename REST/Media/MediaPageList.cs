using Newtonsoft.Json;
using System.Collections.Generic;

namespace WelcomeDAM.REST.Media
{
    public class MediaPageList
    {
        [JsonProperty("data")]
        public List<MediaItem> Assets { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}