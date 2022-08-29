﻿using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Media
{
    public class Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}