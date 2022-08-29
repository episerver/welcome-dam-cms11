using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}