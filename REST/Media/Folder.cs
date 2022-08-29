using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class Folder : BaseRestItem
    {
        [JsonProperty("name")]
        public override string Title { get; set; }

        [JsonProperty("parent_folder_id")]
        public object ParentFolderId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("links")]
        public Link Links { get; set; }
    }
}