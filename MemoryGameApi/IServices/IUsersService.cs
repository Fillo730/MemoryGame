using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface IUsersService
{
    Task<LoginResponseDto?> UpdateProfileAsync(UpdateRequestDto updateRequest, int id);
}
