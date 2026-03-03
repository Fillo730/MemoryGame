using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models; 

namespace MemoryGame_API.Services;

public class UsersService(
    IUserRepository usersRepository,
    ITokenService tokenService,
    IAuthMapper authMapper) : IUsersService
{
    private readonly IUserRepository _usersRepository = usersRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAuthMapper _authMapper = authMapper;

    public async Task<LoginResponseDto?> UpdateProfileAsync(UpdateRequestDto updateRequest, int id)
    {
       
        var user = await _usersRepository.GetUserByIdAsync(id);
        if (user is null) return null;

        
        var userByEmail = await _usersRepository.GetUserByEmailAsync(updateRequest.Email);
        if (userByEmail is not null && userByEmail.Id != id)
        {

            return null;
        }

        
        var userByUsername = await _usersRepository.GetUserByUsernameAsync(updateRequest.Username);
        if (userByUsername is not null && userByUsername.Id != id)
        {
            return null;
        }

        
        user.Username = updateRequest.Username;
        user.Email = updateRequest.Email;

        
        await _usersRepository.SaveChangesAsync();

        
        var token = _tokenService.GenerateToken(user);

        
        return _authMapper.MapUserToLoginResponseDto(user, token);
    }
}