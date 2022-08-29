using Newtonsoft.Json;
using System.Collections.Generic;

namespace WelcomeDAM.REST.Media
{
    public class FolderPageList
    {
        [JsonProperty("data")]
        public List<Folder> Folders { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}