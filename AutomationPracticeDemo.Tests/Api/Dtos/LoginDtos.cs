using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Api.Dtos
{
    public class LoginRequest
    {
        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
