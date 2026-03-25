using System.Text.Json.Serialization;

namespace AutomationPracticeDemo.Tests.Tests.API.Reqres.Dtos;

internal sealed class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
}
