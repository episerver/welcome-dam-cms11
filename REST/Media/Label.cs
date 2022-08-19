using Newtonsoft.Json;
using System.Collections.Generic;

namespace WelcomeDAM.REST.Media
{
    public class Label
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("values")]
        public List<GroupValue> Values { get; set; }
    }
}