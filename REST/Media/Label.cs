using System.Collections.Generic;
using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class Label
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("values")]
        public List<GroupValue> Values { get; set; }
    }
}