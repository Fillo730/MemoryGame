using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto loginRequestDto);

    Task<LoginResponseDto?> RegisterUserAsync(RegisterRequestDto registerRequestDto); 
}
