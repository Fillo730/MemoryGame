using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);

    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(int id);

    Task<IEnumerable<User>> SearchUsersByUsernameAsync(string query, int excludingUserId, int take);

    Task<User?> AddUserAsync(User user);

    Task SaveChangesAsync();
}
