using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class DifficultiesRepository (AppDbContext dbContext) : IDifficultiesRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<IEnumerable<Difficulty>> GetAllDifficultiesAsync(string lang)
    {
        return await _dbContext.Difficulties
            .AsNoTracking()
            .Include(d => d.Translations.Where(dt => dt.LanguageCode == lang))
            .ToListAsync();
    }
}
