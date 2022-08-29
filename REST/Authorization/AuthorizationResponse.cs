using Newtonsoft.Json;

namespace Optimizely.Labs.WelcomeDAM.REST.Authorization
{
    public class AuthorizationResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}