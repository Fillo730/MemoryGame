using MemoryGame_API.Models;

namespace MemoryGame_API.Repositories;

public class BaseRepository (AppDbContext dbContext)
{
    protected readonly AppDbContext _dbContext = dbContext;

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
