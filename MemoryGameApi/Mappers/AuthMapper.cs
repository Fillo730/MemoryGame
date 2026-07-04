using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;
using MemoryGame_API.Utils;

namespace MemoryGame_API.Mappers;

public class AuthMapper : IAuthMapper
{
    public User MapRegisterRequestDtoToUser(RegisterRequestDto registerRequestDto)
    {
        return new User
        {
            Username = registerRequestDto.Username,
            Email = registerRequestDto.Email,
            Role = UserRoleEnum.User,
        };
    }

    public LoginResponseDto MapUserToLoginResponseDto(User user, string token, DateTime expiratesDate)
    {
        return new LoginResponseDto
        {
            Id = user.Id,
            Token = token,
            Username = user.Username,
            Email = user.Email,
            UserRole = user.Role,
            ExpiresDate = expiratesDate
        };
    }
}
