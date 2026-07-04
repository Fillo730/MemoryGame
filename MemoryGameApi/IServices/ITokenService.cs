using MemoryGame_API.Models;

namespace MemoryGame_API.IServices;

public interface ITokenService
{
    (string Token, DateTime Expiration) GenerateToken(User user);
}
