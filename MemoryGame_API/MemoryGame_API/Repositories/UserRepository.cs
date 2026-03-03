using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class UserRepository (AppDbContext dbContext) : BaseRepository(dbContext), IUserRepository
{
    public async Task<User?> AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users.Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }

   
}
