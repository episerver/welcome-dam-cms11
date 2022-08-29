using System.Collections.Generic;
using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class FolderPageList
    {
        [JsonProperty("data")]
        public List<Folder> Folders { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}