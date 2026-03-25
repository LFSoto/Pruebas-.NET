namespace AutomationPracticeDemo.Tests.Tests.API.Reqres.Dtos;

internal sealed class UsersListResponse
{
    public int Page { get; set; }
    public List<UserDto> Data { get; set; } = new();
}
