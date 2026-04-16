using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Api.Dtos
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
