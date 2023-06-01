
using Newtonsoft.Json;

namespace AuthService.Modules.Infrastructure.Integrations.Facebook.Contract
{
    internal class FacebookUserInfoResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}