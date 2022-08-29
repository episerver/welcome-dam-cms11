using System.Configuration;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;

namespace WelcomeDAM.REST.Authorization
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest()
        {
            ClientId = ConfigurationManager.AppSettings["Welcome:Api:ClientId"];
            ClientSecret = ConfigurationManager.AppSettings["Welcome:Api:ClientSecret"];
        }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = "client_credentials";

        [JsonProperty("scope")]
        public string Scope { get; set; } = "scope";

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}