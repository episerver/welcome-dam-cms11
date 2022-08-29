using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class GroupValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}