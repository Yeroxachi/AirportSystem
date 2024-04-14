using Application.DTOs;
using Application.DTOs.User;
using Application.Helpers;
using Application.Response;
using Domain.Enums;
using Domain.Models;

namespace Application.Mapper;

public static class UserMapper
{
    public static User MapToUser(this CreateUserDto dto)
    {
        return new User
        {
            Username = dto.Username,
            UserRole = UserRole.Default,
            PasswordHash = dto.Password.ComputeSha256Hash()
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse
        {
            UserId = user.Id,
            Username = user.Username,
            UserRole = user.UserRole.ToString()
        };
    }
}