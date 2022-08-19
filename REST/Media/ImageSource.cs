using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}