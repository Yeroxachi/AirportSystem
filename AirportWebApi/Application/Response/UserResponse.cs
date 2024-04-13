namespace Application.Response;

public record UserResponse
{
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string UserRole { get; init; }
}