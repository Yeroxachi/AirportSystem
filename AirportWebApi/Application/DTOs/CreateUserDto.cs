namespace Application.DTOs;

public record CreateUserDto
{
    public string Username { get; init; }
    public string Password { get; init; }
}