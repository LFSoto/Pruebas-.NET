using System.Text.Json.Serialization;

namespace AutomationPracticeDemo.Tests.Tests.API.Reqres.Dtos;

internal sealed class UnknownDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("pantone_value")]
    public string PantoneValue { get; set; } = string.Empty;
}
