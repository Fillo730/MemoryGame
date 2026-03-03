using BCrypt.Net;
using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models;
using MemoryGame_API.Repositories;

namespace MemoryGame_API.Services;

public class AuthService (IUserRepository usersRepository, ITokenService tokenService, IAuthMapper authMapper) : IAuthService
{
    private readonly IUserRepository _usersRepository = usersRepository;

    private readonly ITokenService _tokenService = tokenService;

    private readonly IAuthMapper _authMapper = authMapper;
    public async Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _usersRepository.GetUserByUsernameAsync(loginRequestDto.Username);

        if (user == null)
        {
            return null;
        }

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash);

        if(!isPasswordCorrect)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(user);

        return _authMapper.MapUserToLoginResponseDto(user, token);
    }

    public async Task<LoginResponseDto?> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        var userByUsername = await _usersRepository.GetUserByUsernameAsync(registerRequestDto.Username);

        var userByEmail = await _usersRepository.GetUserByEmailAsync(registerRequestDto.Email);

        if(userByUsername != null || userByEmail != null)
        {
            return null;
        }

        var newUser = _authMapper.MapRegisterRequestDtoToUser(registerRequestDto);

        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequestDto.Password);

        await _usersRepository.AddUserAsync(newUser);

        await _usersRepository.SaveChangesAsync();

        var token = _tokenService.GenerateToken(newUser);

        return _authMapper.MapUserToLoginResponseDto(newUser, token);
    }
}
