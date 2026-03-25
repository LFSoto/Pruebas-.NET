using System.Text.Json.Serialization;

namespace AutomationPracticeDemo.Tests.Tests.API.Reqres.Dtos;

internal sealed class UpdateUserResponse
{
    public string Name { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;

    [JsonPropertyName("updatedAt")]
    public string UpdatedAt { get; set; } = string.Empty;
}
