using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}