using Newtonsoft.Json;

namespace WelcomeDAM.REST.Media
{
    public class Pagination
    {
        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }
    }
}