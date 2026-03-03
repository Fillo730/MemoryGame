using MemoryGame_API.Models;

namespace MemoryGame_API.IServices;

public interface ITokenService
{
    string GenerateToken(User user);
}
