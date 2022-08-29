using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class Link
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("parent_folder")]
        public string ParentFolder { get; set; }

        [JsonProperty("child_folders")]
        public string ChildFolders { get; set; }

        [JsonProperty("assets")]
        public string Assets { get; set; }
    }
}