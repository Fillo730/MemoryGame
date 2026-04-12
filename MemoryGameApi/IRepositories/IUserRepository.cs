using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);

    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(int id);

    Task<User?> AddUserAsync(User user);

    Task SaveChangesAsync();
}
