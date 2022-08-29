using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}