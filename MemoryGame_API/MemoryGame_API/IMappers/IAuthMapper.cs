using MemoryGame_API.Dto;
using MemoryGame_API.Models;
using System.Diagnostics;

namespace MemoryGame_API.IMappers;

public interface IAuthMapper
{
    LoginResponseDto MapUserToLoginResponseDto(User user, string token, DateTime expirateDate);

    User MapRegisterRequestDtoToUser(RegisterRequestDto registerRequestDto);
}
